using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdReplay : Bird {

	public List<float> flaps { get; set; }
	private float beginning;
	private int cursor;

	// Called when the bird is started
	protected override void OnStart() {
		beginning = Time.time;
		flaps = DataManager.INSTANCE.gameMode.replayFlaps;
		cursor = 0;
	}

	// Called when the bird is updated
	protected override void OnUpdate () {
		while (cursor < flaps.Count && flaps[cursor] >= (Time.time - beginning)) {
			Fly ();
			cursor ++;
		}
	}
}
