using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bird : MonoBehaviour {

	private Rigidbody2D rbody;

	public float X_speed;
	public float Y_impulse;

	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
		rbody.AddForce (new Vector2(X_speed, 0));

		OnStart ();
	}

	public void OnStart () { }
	
	public void Fly() {
		rbody.AddForce (new Vector2(0, Y_impulse));
	}

	public void Hit() {
		//Debug.Log (name + " HIT !");
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Hit")) {
			Hit ();
		}
	}
}
