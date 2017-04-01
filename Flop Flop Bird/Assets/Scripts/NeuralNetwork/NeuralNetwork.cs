using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork {

	public List<Layer> layers;

	public NeuralNetwork(Genome g) {
		
		layers = new List<Layer>();
		int cursorNeurons = 0;
		int cursorSynapses = 0;

		for (int i = 0; i < g.nbLayers; i++) {
			Layer layer = new Layer (g.neuronsPerLayer[i]);
			layers.Add (layer);

			// Set the previous layer
			if (i > 0) {
				layers[i].SetPrevious(layers[i-1]);
			}

			// Set the threshold
			for (int j = 0 ; j < g.neuronsPerLayer[i] ; j++) {
				layer.neurons[j].threshold = g.thresholds[cursorNeurons];
				cursorNeurons++;

				for (int k = 0; k < layer.neurons [j].inputs.Count; k++) {
					layer.neurons [j].inputs [k].weight = g.weights [cursorSynapses];
					cursorSynapses++;
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
				g.thresholds.Add (layers [i].neurons[j].threshold);

				for (int k = 0; k < layers [i].neurons [j].inputs.Count; k++) {
					g.weights.Add (layers [i].neurons[j].inputs[k].weight);
				}
			}
		}

		return g;
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
				if (layers [i].neurons [j].currentWeight > layers [i].neurons [j].threshold) {
					layers [i].neurons [j].currentWeight = 0;
				}
			}
        }
		return layers [this.layers.Count - 1].ToWeightList ();
    }
}
