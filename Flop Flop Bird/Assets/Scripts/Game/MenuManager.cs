using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public GameObject pauseMenu;
	public GameObject endMenu;
	public GameObject victoryButton;

	public bool pause;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("escape")) {
			pause = true;
			pauseMenu.SetActive (true);
			Time.timeScale = 0;
		}
	}

	public void ShowEndMenu(bool victory) {
		victoryButton.SetActive (victory);
		pause = true;
		endMenu.SetActive (true);
		Time.timeScale = 0;
	}

	public void Resume() {
		if (pause) {
			pause = false;
			pauseMenu.SetActive (false);
			Time.timeScale = 1;
		}
	}

	public void Restart() {
		SceneManager.LoadScene ("Game");
	}

	public void Menu() {
		SceneManager.LoadScene ("InitMenu");
	}

	public void NextLevel() {
		DataManager dataManager = DataManager.INSTANCE;
		dataManager.currentGeneration = dataManager.currentGeneration.NextGeneration ();
		dataManager.generationNb++;

		SceneManager.LoadScene ("Game");
	}
}
