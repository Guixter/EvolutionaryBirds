using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork {

	public List<Layer> layers;

	public NeuralNetwork(Genome g) {
		
		layers = new List<Layer>();

		for (int i = 0; i < g.nbLayers; i++) {
			Layer layer = new Layer (g.neuronsPerLayer[i]);
			layers.Add (layer);

			// Set the previous layer
			if (i > 0) {
				layers[i].SetPrevious(layers[i-1]);
			}

			// Set the weights
			for (int j = 0 ; j < g.neuronsPerLayer[i] ; j++) {
				for (int k = 0; k < layer.neurons [j].inputs.Count; k++) {
					layer.neurons [j].inputs [k].weight = g.weights [k];
				}
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
					g.weights.Add (layers [i].neurons[j].inputs[k].weight);
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
		for (int i = 1; i < layers.Count ; i++) {
			for (int j = 0; j < layers[i].neurons.Count; j++) {
				layers [i].neurons [j].currentWeight = 0;
				for (int k = 0; k < layers[i].neurons[j].inputs.Count; k++) {
					layers[i].neurons[j].currentWeight += layers[i].neurons[j].inputs[k].weight*layers[i].neurons[j].inputs[k].input.currentWeight;
				}
				layers[i].neurons[j].currentWeight = NeuralNetwork.Sigmoid(layers[i].neurons[j].currentWeight);
			}
        }
		return layers [this.layers.Count - 1].ToWeightList ();
    }
}
