using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The Bird Player class.
 */
public class BirdPlayer : Bird {

	public List<float> flaps { get; set; }
	private float beginning;

	protected override void OnStart() {
		flaps = new List<float> ();
		beginning = Time.time;
	}

	// Called when the bird is updated
	protected override void OnUpdate () {
		if (Input.GetKeyDown ("space")) {
			flaps.Add (Time.time - beginning);
			Fly ();
		}
	}

}
