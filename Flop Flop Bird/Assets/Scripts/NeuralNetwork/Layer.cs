using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer {

	public List<Neuron> neurons;
	public Layer previousLayer;

	public Layer(int nbNeurons) {
		neurons = new List<Neuron> ();
		for (int i = 0; i < nbNeurons; i++) {
			neurons.Add (new Neuron());
		}
	}

	public void SetPrevious(Layer previous) {
		previousLayer = previous;
		for (int i = 0; i < neurons.Count; i++) {
			neurons[i].SetPreviousNeurons (previous.neurons);
		}
	}
}
