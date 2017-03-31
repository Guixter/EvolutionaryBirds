using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPlayer : Bird {

	public override void OnUpdate () {
		if (Input.GetKeyDown ("space")) {
			Fly ();
		}
	}

}
