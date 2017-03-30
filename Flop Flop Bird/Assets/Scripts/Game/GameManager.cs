using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private DataManager dataManager;
	private GeneticManager geneticManager;
	private GameObject player;
	private float screenWidth;
	private float screenHeight;
	private float camXOffset;
	private float decorMaxCovered;
	private GameObject decorLeft, decorMiddle;


	public GameObject AIPrefab;
	public GameObject PlayerPrefab;
	public GameObject BackgroundPrefab;

	////////////////////////////////////////////////////////////////

	// Start the game
	void Start () {
		dataManager = DataManager.INSTANCE;
		screenHeight = 2 * Camera.main.orthographicSize;
		screenWidth = screenHeight * Camera.main.aspect;

		decorMaxCovered = - screenWidth/2;
		decorLeft = null;
		decorMiddle = null;

		SpawnIAs ();
		SpawnPlayer ();
	}

	// Spawn the IAs
	private void SpawnIAs() {
		int i = 1;
		foreach (Genome g in dataManager.currentGeneration.genomes) {
			GameObject o = Instantiate (AIPrefab);
			o.name = "BirdAI " + i;
			o.GetComponent<BirdAI> ().SetNeural (g);
			o.transform.SetParent (GameObject.Find("Birds").transform);
			i++;
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

	// Update
	void Update () {
		UpdateCamera ();
		UpdateDecor ();
		// Spawn pipes
	}

	////////////////////////////////////////////////////////////////

	// When the level is ended
	public void EndOfLevel () {
		dataManager.currentGeneration = dataManager.currentGeneration.NextGeneration ();
	}
}
