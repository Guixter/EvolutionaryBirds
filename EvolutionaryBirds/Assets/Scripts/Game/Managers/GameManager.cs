using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	// Private attributes
	private DataManager dataManager;
	private MenuManager menuManager;
	private List<GameObject> bots;
	private float camXOffset;
	private float decorMaxCovered;
	private float pipesMaxCovered;
	private float semiPipeWidth;
	private int lastGateCrossed;
	private int replayPipeCursor;

	// Parameters
	public GameObject AIPrefab, PlayerPrefab, ReplayPrefab, BackgroundPrefab, PipePrefab;
	public float PipesXGap, PipesYGap;
	public Text GUIAliveBots, GUIPipes, GUIFitness, GUIGeneration;
	public GameObject timeUI;

	// Properties
	public GameObject nextPipe { get; set; }
	public float screenWidth { get; set; }
	public float screenHeight { get; set; }
	public GameMode gameMode { get; set; }
	public GameObject player { get; set; }
	public List<GameObject> pipes { get; set; }

	////////////////////////////////////////////////////////////////

	// Start the game
	void Start () {
		// Get the managers
		dataManager = DataManager.INSTANCE;
		menuManager = GetComponent<MenuManager> ();
		gameMode = dataManager.gameMode;

		// Set some properties
		screenHeight = 2 * Camera.main.orthographicSize;
		screenWidth = screenHeight * Camera.main.aspect;
		pipes = new List<GameObject> ();
		pipesMaxCovered = 0;
		lastGateCrossed = -1;
		decorMaxCovered = - screenWidth/2;
		semiPipeWidth = PipePrefab.GetComponentInChildren<SpriteRenderer> ().bounds.extents.x;
		replayPipeCursor = 0;

		// Set the timescale
		Time.timeScale = gameMode.timeSpeed;
		if (gameMode.mode == GameMode.Modes.ONE_VS_ALL) {
			Cursor.visible = false;
		}

		// Print the current generation
		GUIGeneration.text = "Generation " + dataManager.generationNb;

		// Handle the game mode
		HandleGameMode ();
	}

	private void HandleGameMode() {
		SpawnIAs ();
		if (gameMode.mode == GameMode.Modes.ONE_VS_ALL) {
			if (gameMode.replayBird) {
				SpawnReplay ();
			} else {
				SpawnPlayer ();
			}
		} else if (gameMode.mode == GameMode.Modes.SIMULATION) {
			player = bots [0];
			timeUI.SetActive (true);
		}
	}

	// Spawn the IAs
	private void SpawnIAs() {
		bots = new List<GameObject> ();
		int i = 1;
		foreach (Genome g in dataManager.currentGeneration.genomes) {
			GameObject o = Instantiate (AIPrefab);
			o.name = "BirdAI " + i;
			o.GetComponent<BirdAI> ().SetNeural (g);
			o.transform.SetParent (GameObject.Find("Birds").transform);
			i++;
			bots.Add (o);
		}
	}

	// Spawn the player
	private void SpawnPlayer() {
		player = Instantiate (PlayerPrefab);
		player.name = "BirdPlayer";
		camXOffset = Camera.main.transform.position.x - player.transform.position.x;
		player.transform.SetParent (GameObject.Find("Birds").transform);
	}

	// Spawn the replay
	private void SpawnReplay() {
		player = Instantiate (ReplayPrefab);
		player.GetComponent<BirdReplay> ().flaps = gameMode.replayFlaps;
		player.name = "BirdReplay";
		camXOffset = Camera.main.transform.position.x - player.transform.position.x;
		player.transform.SetParent (GameObject.Find("Birds").transform);
	}

	////////////////////////////////////////////////////////////////

	// Update the camera
	private void UpdateCamera() {
		Vector3 newPos = new Vector3 (player.transform.position.x + camXOffset, Camera.main.transform.position.y, Camera.main.transform.position.z);
		Camera.main.transform.position = newPos;
	}

	// Update the decor
	private void UpdateDecor() {
		if (Camera.main.transform.position.x + BackgroundPrefab.GetComponent<SpriteRenderer>().bounds.extents.x >= decorMaxCovered) {
			GameObject o = Instantiate (BackgroundPrefab);
			o.transform.position = new Vector3 (decorMaxCovered + screenWidth/2, o.transform.position.y, o.transform.position.z);
			o.transform.SetParent (GameObject.Find("Decors").transform);
			decorMaxCovered += BackgroundPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
		}
	}

	// Update the pipes
	private void UpdatePipes() {
		while (pipesMaxCovered < decorMaxCovered) {
			// Chose the pipe's position
			pipesMaxCovered += PipesXGap;
			float yPos;
			if (gameMode.replayBird && replayPipeCursor < gameMode.replayPipes.Count) {
				yPos = gameMode.replayPipes [replayPipeCursor];
				replayPipeCursor++;
			} else {
				yPos = Random.Range(-.4f, .4f) * (screenHeight - PipesYGap);
			}

			// Spawn the pipe
			GameObject o = Instantiate(PipePrefab);
			o.transform.position = new Vector3 (pipesMaxCovered, o.transform.position.y, o.transform.position.z);
			o.transform.SetParent (GameObject.Find("Pipes").transform);
			o.name = "Pipe " + (pipes.Count + 1);
			o.GetComponent<Pipe> ().Initialize (pipesMaxCovered, yPos, PipesYGap);

			pipes.Add (o);
		}
	}

	// Update the GUI
	private void UpdateGUI() {
		// Update the alive bots
		int aliveBots = 0;
		foreach (GameObject o in bots) {
			if (!o.GetComponent<Bird> ().dead) {
				aliveBots++;
			}
		}
		GUIAliveBots.text = "Bots : " + aliveBots + "/" + bots.Count;

		// Update the pipes
		while ((pipes.Count > lastGateCrossed+1) && ((pipes[lastGateCrossed+1].transform.position.x + semiPipeWidth) <= player.transform.position.x)) {
			lastGateCrossed ++;
		}
		GUIPipes.text = "" + (lastGateCrossed + 1);
		nextPipe = pipes[lastGateCrossed+1];

		// Update the fitness
		GUIFitness.text = "Fitness : " + player.GetComponent<Bird>().fitness.ToString("F2");
	}

	// Update the menus
	private void UpdateMenus() {
		if (gameMode.mode == GameMode.Modes.ONE_VS_ALL) {
			if (player.GetComponent<Bird> ().dead) {
				menuManager.ShowEndMenu (false);
			} else {
				bool victory = true;

				foreach (GameObject bot in bots) {
					if (!bot.GetComponent<Bird> ().dead) {
						victory = false;
						break;
					}
				}

				if (victory) {
					menuManager.ShowEndMenu (true);
				}
			}

		} else if (gameMode.mode == GameMode.Modes.SIMULATION && player.GetComponent<Bird> ().dead) {
			foreach (GameObject bot in bots) {
				if (!bot.GetComponent<Bird> ().dead) {
					player = bot;
					return;
				}
			}

			menuManager.NextLevel ();
		}

		if (Input.GetKeyDown ("escape")) {
			menuManager.ShowPauseMenu ();
		}
	}

	// Update
	void Update () {
		UpdateCamera ();
		UpdateDecor ();
		UpdatePipes ();
		UpdateGUI ();
		UpdateMenus ();
	}

}
