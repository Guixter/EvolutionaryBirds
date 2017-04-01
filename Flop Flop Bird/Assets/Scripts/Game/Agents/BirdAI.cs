using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The Bird AI class.
 */
public class BirdAI : Bird {
	
	// Private attributes
	private float flapTime;

	// Parameters
	public float FlapPeriod;

	// Properties
	public Genome g { get; set; }
	public NeuralNetwork network { get; set; }

	////////////////////////////////////////////////////////////////

	// Called when the bird is started
	protected override void OnStart() {
		flapTime = Time.time;
	}

	// Called when the bird is updated
	protected override void OnUpdate () {
		if (Time.time >= flapTime + FlapPeriod) {
			Fly ();
			flapTime = Time.time;
		}
	}

	// Set the AI's neural network
	public void SetNeural(Genome g) {
		this.g = g;
		this.network = new NeuralNetwork (g);
	}

	public override void Hit() {
		base.Hit ();
		g.fitness = fitness;
	}
}
