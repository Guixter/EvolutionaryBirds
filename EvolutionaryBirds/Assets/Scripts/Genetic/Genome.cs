using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * A Genome is the description of a bird's neural network.
 */
public class Genome {

	public float fitness;

	// The threshold
	public float threshold;

	// The number of layers in the neural network, including the input and the output
	public int nbLayers;

	// The number of neurons in each layer
	public List<int> neuronsPerLayer;

	// The weights of the synapses
	public List<float> weights;

	// Build the genome
	public Genome() {
		nbLayers = 0;
		fitness = 0;
		threshold = 0;
		neuronsPerLayer = new List<int> ();
		weights = new List<float> ();
	}

	// Clone the genome
	public Genome Clone() {
		Genome clone = new Genome ();

		clone.nbLayers = nbLayers;
		clone.fitness = 0;
		clone.threshold = threshold;

		foreach (int neurons in neuronsPerLayer) {
			clone.neuronsPerLayer.Add (neurons);
		}

		foreach (float weight in weights) {
			clone.weights.Add (weight);
		}

		return clone;
	}

	// Create a random genome respecting a given structure
	public static Genome RandomGenome(List<int> structure) {
		Genome g = new Genome ();
		
		g.nbLayers = structure.Count;
		g.neuronsPerLayer = new List<int>(structure);
		g.threshold = Random.Range (-1.0f, 1.0f);

		for (int i = 0; i < g.nbLayers; i++) { // layers
			for (int j = 0; j < structure[i]; j++) { // neurons
				g.weights.Add(Random.Range(-1.0f, 1.0f));
			}
		}
		return g;
	}
}
