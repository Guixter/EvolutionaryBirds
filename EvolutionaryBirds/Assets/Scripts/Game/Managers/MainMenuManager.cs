using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * The MainMenu manager.
 */
public class MainMenuManager : MonoBehaviour {

	// Start
	void Start() {
		Cursor.visible = true;
	}

	// One vs All mode
	public void OneVsAll() {
		DataManager dataManager = DataManager.INSTANCE;
		dataManager.currentGeneration.RandomGeneration ();
		dataManager.generationNb = 1;

		dataManager.gameMode.mode = GameMode.Modes.ONE_VS_ALL;

		SceneManager.LoadScene ("Game");
	}

	// Simulation mode
	public void Simulation() {
		DataManager dataManager = DataManager.INSTANCE;
		dataManager.currentGeneration.RandomGeneration ();
		dataManager.generationNb = 1;

		dataManager.gameMode.mode = GameMode.Modes.SIMULATION;

		SceneManager.LoadScene ("Game");
	}

	// Exit Game
	public void Exit() {
		Application.Quit ();
	}
}
