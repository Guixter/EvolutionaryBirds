using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * The menu manager.
 */
public class MenuManager : MonoBehaviour {

	// Private attributes
	private BirdAI bot;
	private DataManager dataManager;

	// Parameters
	public GameObject pauseMenu;
	public GameObject endMenu;
	public GameObject victoryButton;
	public GameObject botStats;
	public Text leftColumn, midColumn, rightColumn;

	// Properties
	public bool pause { get; set; }

	////////////////////////////////////////////////////////////////

	// Start
	void Start() {
		dataManager = DataManager.INSTANCE;
	}

	// Update
	void Update() {
		if (bot != null) {
			DrawStats ();
		}
	}

	////////////////////////////////////////////////////////////////

	// Show the end menu (whether it is a victory or not)
	public void ShowEndMenu(bool victory) {
		victoryButton.SetActive (victory);
		pause = true;
		endMenu.SetActive (true);
		Time.timeScale = 0;
	}

	// Show the pause menu
	public void ShowPauseMenu() {
		pause = true;
		pauseMenu.SetActive (true);
		Time.timeScale = 0;
	}

	// Show the bot stats
	public void ShowBotStats(BirdAI bot) {
		this.bot = bot;
		botStats.SetActive(true);
		//Time.timeScale = 0;
	}

	// Hide the bot stats
	public void HideBotStats() {
		bot = null;
		botStats.SetActive (false);
		//Time.timeScale = dataManager.timeSpeed;
	}

	// Draw a bot stats
	public void DrawStats() {
		NeuralNetwork network = bot.network;
		string txt;

		// Show the current values
		txt = "";
		foreach (Neuron n in network.layers[0].neurons) {
			txt += n.value.ToString("F2") + "\n";
			txt += " (" + n.weight.ToString ("F2") + ")" + "\n\n";
		}
		leftColumn.text = txt;

		txt = "";
		foreach (Neuron n in network.layers[1].neurons) {
			txt += n.value.ToString("F2") + "\n";
			txt += " (" + n.weight.ToString ("F2") + ")" + "\n\n";
		}
		midColumn.text = txt;

		txt = "";
		foreach (Neuron n in network.layers[2].neurons) {
			txt += n.value.ToString("F2") + "\n";
			txt += " (" + n.weight.ToString ("F2") + ")" + "\n\n";
		}
		rightColumn.text = txt;
	}

	////////////////////////////////////////////////////////////////

	// Resume the game
	public void Resume() {
		if (pause) {
			pause = false;
			pauseMenu.SetActive (false);
			Time.timeScale = dataManager.timeSpeed;
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
		dataManager.currentGeneration = dataManager.currentGeneration.NextGeneration ();
		dataManager.generationNb++;

		SceneManager.LoadScene ("Game");
	}

	// Change the time speed
	public void SetTimeSpeed(float t) {
		dataManager.timeSpeed = t;
		Time.timeScale = t;
		Time.fixedDeltaTime = t * 0.02f;
	}

	// Set whether there is a player or not
	public void SetPlaying(bool p) {
		dataManager.playing = p;
	}
}
