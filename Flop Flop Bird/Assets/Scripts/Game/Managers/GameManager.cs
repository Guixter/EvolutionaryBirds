using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	// Private attributes
	private DataManager dataManager;
	private GameObject player;
	private List<GameObject> bots;
	private float screenWidth;
	private float screenHeight;
	private float camXOffset;
	private float decorMaxCovered;
	private float pipesMaxCovered;
	private GameObject decorLeft, decorMiddle;
	private List<GameObject> pipes;
	private int lastGateCrossed;

	// Parameters
	public GameObject AIPrefab, PlayerPrefab, BackgroundPrefab, PipePrefab;
	public float PipesXGap, PipesYGap;
	public Text GUIAliveBots, GUIPipes, GUIFitness, GUIGeneration;

	////////////////////////////////////////////////////////////////

	// Start the game
	void Start () {
		Time.timeScale = 1;
		dataManager = DataManager.INSTANCE;
		screenHeight = 2 * Camera.main.orthographicSize;
		screenWidth = screenHeight * Camera.main.aspect;
		GUIGeneration.text = "Generation " + dataManager.generationNb;

		pipes = new List<GameObject> ();
		pipesMaxCovered = 0;
		lastGateCrossed = -1;

		decorMaxCovered = - screenWidth/2;
		decorLeft = null;
		decorMiddle = null;

		SpawnIAs ();
		SpawnPlayer ();
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

	////////////////////////////////////////////////////////////////

	// Update the camera
	private void UpdateCamera() {
		Vector3 newPos = new Vector3 (player.transform.position.x + camXOffset, Camera.main.transform.position.y, Camera.main.transform.position.z);
		Camera.main.transform.position = newPos;
	}

	// Update the decor
	private void UpdateDecor() {
		if (Camera.main.transform.position.x + screenWidth / 2 >= decorMaxCovered) {
			GameObject o = Instantiate (BackgroundPrefab);
			o.transform.position = new Vector3 (decorMaxCovered + screenWidth/2, o.transform.position.y, o.transform.position.z);
			o.transform.SetParent (GameObject.Find("Decors").transform);
			decorMaxCovered += screenWidth;

			if (decorLeft != null) {
				Destroy (decorLeft);
			}
			decorLeft = decorMiddle;
			decorMiddle = o;
		}
	}

	// Update the pipes
	private void UpdatePipes() {
		while (pipesMaxCovered < decorMaxCovered) {
			// Chose the pipe's position
			pipesMaxCovered += PipesXGap;
			float yPos = (Random.value - .5f) * (screenHeight - PipesYGap);

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
		while ((pipes.Count > lastGateCrossed+1) && (pipes[lastGateCrossed+1].transform.position.x <= player.transform.position.x)) {
			lastGateCrossed ++;
		}
		GUIPipes.text = "" + (lastGateCrossed + 1);

		// Update the fitness
		GUIFitness.text = "Fitness : " + player.GetComponent<Bird>().fitness.ToString("F2");
	}

	// Update the menus
	private void UpdateMenus() {
		if (player.GetComponent<Bird> ().dead) {
			bool victory = true;

			foreach (GameObject bot in bots) {
				if (!bot.GetComponent<Bird> ().dead) {
					victory = false;
					break;
				}
			}

			GetComponent<MenuManager> ().ShowEndMenu (victory);
		} else if (Input.GetKeyDown("escape")) {
			GetComponent<MenuManager> ().ShowPauseMenu ();
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
