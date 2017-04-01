using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * A Genome is the description of a bird's neural network.
 */
public class Genome {

	public float fitness;

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
		neuronsPerLayer = new List<int> ();
		weights = new List<float> ();
		thresholds = new List<float> ();
	}

	// Clone the genome
	public Genome Clone() {
		Genome clone = new Genome ();

		clone.nbLayers = nbLayers;
		clone.fitness = 0;

		foreach (int neurons in neuronsPerLayer) {
			clone.neuronsPerLayer.Add (neurons);
		}

		foreach (float weight in weights) {
			clone.weights.Add (weight);
		}

		foreach (float threshold in thresholds) {
			clone.thresholds.Add (threshold);
		}

		return clone;
	}
	/**
	 * structure ex : [2,2,1]
	 */
	public static Genome RandomGenome(List<int> structure) {
		Genome g = new Genome ();
		
		g.nbLayers = structure.Count;
		g.neuronsPerLayer = structure;

		List<float> weights = new List<float>();
		for (int i = 0; i < g.nbLayers; i++) { // layers
			for (int j = 0; j < structure[i]; j++) { // neurons
				g.weights.Add(Random.Range(-1.0f, 1.0f););
			}
		}
		g.weights = weights;
		return g;
	}
}
