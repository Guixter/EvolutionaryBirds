using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPlayer : Bird {

	void Update () {
		if (Input.GetKeyDown ("space")) {
			Fly ();
		}
	}

}
