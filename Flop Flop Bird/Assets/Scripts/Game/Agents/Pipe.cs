using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

	// Parameters
	public GameObject upPipe;
	public GameObject downPipe;

	// Properties
	public float xPos { get; set; }
	public float yPos { get; set; }

	////////////////////////////////////////////////////////////////

	// Initialize the pipe
	public void Initialize (float xPos, float yPos, float yGap) {
		this.xPos = xPos;
		this.yPos = yPos;

		Vector3 pipeExtents = upPipe.GetComponent<SpriteRenderer> ().bounds.extents;
		upPipe.transform.position = new Vector3 (xPos, yPos + yGap/2 + pipeExtents.y, upPipe.transform.position.z);
		downPipe.transform.position = new Vector3 (xPos, yPos - yGap/2 - pipeExtents.y, downPipe.transform.position.z);
	}
}
