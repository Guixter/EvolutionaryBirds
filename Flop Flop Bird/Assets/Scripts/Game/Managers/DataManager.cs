using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The DataManager is a Singleton.
 * It contains all the data that needs to be saved between several scenes.
 */
public class DataManager : MonoBehaviour {

	// The DataManager instance
	public static DataManager INSTANCE;

	// Properties
	public GameMode gameMode { get; set; }
	public Generation currentGeneration { get; set; }
	public int generationNb { get; set; }

	////////////////////////////////////////////////////////////////

	// Awake method
	void Awake () {
		if (INSTANCE != null) {
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		INSTANCE = this;

		gameMode = new GameMode ();
		currentGeneration = new Generation ();
		currentGeneration.RandomGeneration ();
		generationNb = 1;
	}
}
