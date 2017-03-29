using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * A Genome is the description of a bird's neural network.
 */
public class Genome {

	// The number of layers in the neural network, including the input and the output
	public int nbLayers;

	// The number of neurons in each layer
	public List<int> neuronsPerLayer;

	// The weights of the neurons
	public List<float> weights;

	// Clone the genome
	public Genome Clone() {
		// TODO (Guillaume)
	}
}
