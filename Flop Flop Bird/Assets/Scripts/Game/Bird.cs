using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bird : MonoBehaviour {

	private Rigidbody2D rbody;
	private SpriteRenderer render;

	public float X_speed;
	public float Y_impulse;
	public float AnimationFlapTime;
	public Sprite AnimationIdle, AnimationFlap;

	public bool dead;
	public float fitness;
	private float startX;
	private float lastFlap;

	void Start () {
		dead = false;
		rbody = GetComponent<Rigidbody2D> ();
		rbody.AddForce (new Vector2(X_speed, 0));
		startX = transform.position.x;
		render = GetComponent<SpriteRenderer> ();
		lastFlap = -1;

		OnStart ();
	}

	void Update() {
		// Update the fitness
		fitness = transform.position.x - startX;

		// Update the sprite
		if (lastFlap != -1 && Time.time >= lastFlap + AnimationFlapTime) {
			render.sprite = AnimationIdle;
			lastFlap = -1;
		}

		OnUpdate ();
	}

	public virtual void OnStart () { }

	public virtual void OnUpdate () { }
	
	public void Fly() {
		if (!dead) {
			rbody.AddForce (new Vector2 (0, Y_impulse));
			render.sprite = AnimationFlap;
			lastFlap = Time.time;
		}
	}

	public void Hit() {
		rbody.simulated = false;
		dead = true;
		//Debug.Log (name + " HIT ! Fitness : " + fitness);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Hit")) {
			Hit ();
		}
	}
}
