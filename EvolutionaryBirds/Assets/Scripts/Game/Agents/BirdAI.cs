﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The Bird AI class.
 */
public class BirdAI : Bird {
	
	// Private attributes
	private float flapTime;
	private GameManager gameManager;

	// Parameters
	public Color color1, color2;

	// Properties
	public Genome g { get; set; }
	public NeuralNetwork network { get; set; }

	////////////////////////////////////////////////////////////////

	// Called when the bird is started
	protected override void OnStart() {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		GetComponent<SpriteRenderer> ().color = Color.Lerp (color1, color2, Random.value);
	}

	// Called when the bird is clicked
	/*void OnMouseDown() {
		MenuManager m = GameObject.Find ("GameManager").GetComponent<MenuManager> ();
		m.ShowBotStats (this);
	}*/

	// Called when the bird is updated
	protected override void OnUpdate () {
		if (gameManager.nextPipe != null) {
			float[] inputs = new float[2];
			inputs [0] = transform.position.y / gameManager.screenHeight;
			inputs [1] = - gameManager.nextPipe.GetComponent<Pipe>().yPos / gameManager.screenHeight;
			List<float> result = network.Forward (inputs);

			if (result.Count > 0 && result [0] > .4f) {
				Fly ();
			}
		}
	}

	// Set the AI's neural network
	public void SetNeural(Genome g) {
		this.g = g;
		this.network = new NeuralNetwork (g);
	}

	// Called when the bird hits an obstacle
	public override void Hit() {
		base.Hit ();
		g.fitness = fitness;
	}
}
