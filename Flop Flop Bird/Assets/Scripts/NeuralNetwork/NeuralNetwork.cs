using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork {

	public List<Layer> layers;

	public NeuralNetwork(Genome g) {

		layers = new List<Layer>();

		int cursor = 0;
		for (int i = 0; i < g.nbLayers; i++) {
			Layer layer = new Layer (g.neuronsPerLayer[i]);
			layers.Add (layer);

			// Set the previous layer
			if (i > 0) {
				layers[i].SetPrevious(layers[i-1]);
			}

			// Set the weights
			for (int j = 0 ; j < g.neuronsPerLayer[i] ; j++) {
				layer.neurons [j].weight = g.weights [cursor];
				cursor++;
			}
		}
	}

	public Genome ToGenome() {
		Genome g = new Genome ();

		g.nbLayers = layers.Count;

		for (int i = 0; i < g.nbLayers; i++) {
			g.neuronsPerLayer.Add (layers[i].neurons.Count);
			for (int j = 0; j < layers [i].neurons.Count; j++) {
				for (int k = 0; k < layers [i].neurons [j].inputs.Count; k++) {
					g.weights.Add (layers [i].neurons[j].inputs[k].input.weight);
				}
			}
		}

		return g;
	}

	public static float Sigmoid(float x) {
		return 1.0f / (1.0f + Mathf.Exp(-x)); 
	}

	/**
	 * 
	 */
	public List<float> Forward(float[] inputs) {
		for (int i = 0 ; i < inputs.Length ; i++) {
			layers [0].neurons [i].value = inputs [i];
		}

		for (int i = 1; i < layers.Count ; i++) {
			for (int j = 0; j < layers[i].neurons.Count; j++) {
				layers [i].neurons [j].value = 0;
				for (int k = 0; k < layers[i].neurons[j].inputs.Count; k++) {
					layers[i].neurons[j].value += layers[i].neurons[j].weight * layers[i].neurons[j].inputs[k].input.value;
				}
				layers[i].neurons[j].value = NeuralNetwork.Sigmoid(layers[i].neurons[j].value);
			}
        }
		return layers [this.layers.Count - 1].ToWeightList ();
    }
}
