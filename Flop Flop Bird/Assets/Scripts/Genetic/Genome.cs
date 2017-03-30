using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * A Genome is the description of a bird's neural network.
 */
public class Genome {

	public float score;

	// The number of layers in the neural network, including the input and the output
	public int nbLayers;

	// The number of neurons in each layer
	public List<int> neuronsPerLayer;

	// The weights of the neurons
	public List<float> weights;

	// Build the genome
	public Genome() {
		nbLayers = 0;
		score = 0;
		neuronsPerLayer = new List<int> ();
		weights = new List<float> ();
	}

	// Clone the genome
	public Genome Clone() {
		Genome clone = new Genome ();

		clone.nbLayers = nbLayers;
		clone.score = score;

		foreach (int neurons in neuronsPerLayer) {
			clone.neuronsPerLayer.Add (neurons);
		}

		foreach (float weight in weights) {
			clone.weights.Add (weight);
		}

		return clone;
	}
}
