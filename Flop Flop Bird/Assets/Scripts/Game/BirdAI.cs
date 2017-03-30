using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAI : Bird {

	public Genome g;
	public float period;
	private float lastFlap;

	new public void OnStart() {
		lastFlap = Time.time + period*Random.value;
	}

	public void SetNeural(Genome g) {

	}

	void Update () {
		if (Time.time >= lastFlap + period) {
			Fly ();
			lastFlap = Time.time;
		}
	}
}
