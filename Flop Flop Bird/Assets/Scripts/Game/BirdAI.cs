using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAI : Bird {

	public Genome g;
	public float period;
	private float lastFlap2;

	public override void OnStart() {
		lastFlap2 = Time.time;
	}

	public override void OnUpdate () {
		if (Time.time >= lastFlap2 + period) {
			Fly ();
			lastFlap2 = Time.time;
		}
	}

	public void SetNeural(Genome g) {

	}
}
