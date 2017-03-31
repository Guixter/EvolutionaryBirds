using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

	public GameObject upPipe;
	public GameObject downPipe;

	public float xPos;
	public float yPos;

	// Initialize the pipe
	public void Initialize (float xPos, float yPos, float yGap) {
		this.xPos = xPos;
		this.yPos = yPos;

		Vector3 pipeExtents = upPipe.GetComponent<SpriteRenderer> ().bounds.extents;
		upPipe.transform.position = new Vector3 (xPos, yPos + yGap/2 + pipeExtents.y, upPipe.transform.position.z);
		downPipe.transform.position = new Vector3 (xPos, yPos - yGap/2 - pipeExtents.y, downPipe.transform.position.z);
	}
}
