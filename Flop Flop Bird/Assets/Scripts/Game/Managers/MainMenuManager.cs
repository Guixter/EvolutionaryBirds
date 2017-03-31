using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * The MainMenu manager.
 */
public class MainMenuManager : MonoBehaviour {

	// Play the game
	public void Play() {
		DataManager dataManager = DataManager.INSTANCE;
		dataManager.currentGeneration.RandomGeneration ();
		dataManager.generationNb = 1;

		SceneManager.LoadScene ("Game");
	}
}
