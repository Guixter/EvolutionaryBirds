using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * The menu manager.
 */
public class MenuManager : MonoBehaviour {

	// Parameters
	public GameObject pauseMenu;
	public GameObject endMenu;
	public GameObject victoryButton;

	// Properties
	public bool pause { get; set; }

	////////////////////////////////////////////////////////////////

	// Show the end menu (whether it is a victory or not)
	public void ShowEndMenu(bool victory) {
		victoryButton.SetActive (victory);
		pause = true;
		endMenu.SetActive (true);
		Time.timeScale = 0;
	}

	public void ShowPauseMenu() {
		pause = true;
		pauseMenu.SetActive (true);
		Time.timeScale = 0;
	}

	////////////////////////////////////////////////////////////////

	// Resume the game
	public void Resume() {
		if (pause) {
			pause = false;
			pauseMenu.SetActive (false);
			Time.timeScale = 1;
		}
	}

	// Restart the level
	public void Restart() {
		SceneManager.LoadScene ("Game");
	}

	// Go to the main menu
	public void Menu() {
		SceneManager.LoadScene ("MainMenu");
	}

	// Go to the next level
	public void NextLevel() {
		DataManager dataManager = DataManager.INSTANCE;
		dataManager.currentGeneration = dataManager.currentGeneration.NextGeneration ();
		dataManager.generationNb++;

		SceneManager.LoadScene ("Game");
	}
}
