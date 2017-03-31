using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The DataManager is a Singleton.
 * It contains all the data that needs to be saved between several scenes.
 */
public class DataManager : MonoBehaviour {

	public static DataManager INSTANCE;
	public Generation currentGeneration;
	public int generationNb;

	void Awake () {
		DontDestroyOnLoad(gameObject);

		if (INSTANCE == null) {
			INSTANCE = this;
		} else {
			Destroy (gameObject);
			return;
		}

		currentGeneration = new Generation ();
		currentGeneration.RandomGeneration ();
		generationNb = 1;
	}
}
