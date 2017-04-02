using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode {

	public enum Modes {ONE_VS_ALL, SIMULATION};

	public Modes mode;

	public float timeSpeed { get; set; }

	public bool replayBird { get; set; }
	public List<float> replayFlaps { get; set; }

	public GameMode() {
		mode = Modes.SIMULATION;
		timeSpeed = 1;

		replayBird = false;
		replayFlaps = null;
	}
}
