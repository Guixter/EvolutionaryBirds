using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The Bird Player class.
 */
public class BirdPlayer : Bird {

	// Called when the bird is updated
	protected override void OnUpdate () {
		if (Input.GetKeyDown ("space")) {
			Fly ();
		}
	}

}
