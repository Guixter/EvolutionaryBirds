using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedForward {
	
    private class Layer {

        public float[] biases;
        public float[] weights;
		public Layer previousLayer;

        /**
         *
         */
        public Layer(int weights, int biases, int[] range) {
            this.biases = new float[biases];
            this.weights = new float[weights];
            for (int i = 0; i < weights; i++) {
				if (range.Length == 1) {
					this.weights[i] = Random.Range(0, range[0]) * .1f;
                } else if (range.Length == 2) {
					this.weights[i] = Random.Range(range[0], range[1]) * .1f;
                } else {
					this.weights[i] = Random.Range(-1.0f, 1.0f) * .1f;
                }
            }
            for (int i = 0; i < biases; i++) {
                if (range.Length == 1) {
					this.biases[i] = Random.Range(0, range[0]) * .1f;
                } else if (range.Length == 2) {
					this.biases[i] = Random.Range(range[0], range[1]) * .1f;
                } else {
                    this.biases[i] = Random.Range(-1.0f, 1.0f) * .1f;
                }
            }
        }

        public static float Sigmoid(float x) {
            return 1.0f / (1.0f + Mathf.Exp(-x));
        }

        public void Feed(Layer previous) {
            float prev = 0.0f;
            for (int i = 0; i < previous.weights.Length; i++) {
                prev += previous.weights[i];
            }
            prev = Sigmoid(prev);
            for (int i = 0; i < weights.Length; i++) {
                this.weights[i] += prev;
            }
        }
    }

    private Layer[] layers;
    private float LearningRate;

    /* example1 : layers : [[2],[2],[1]] =>
     * inputLayer : 2 neurons
     * hiddenLayer 1 : 2 neurons , 0 bias
     * outputLayer : 1 neuron
     */
    /* example2 : layers : [[2],[2,1],[1]] =>
     * inputLayer : 2 neurons
     * hiddenLayer 1 : 2 neurons , 1 bias
     * outputLayer : 1 neuron
     */
    public FeedForward(int[][] layers, int[] range) {
        for (int i = 0; i < layers.Length; i++) {
            if (layers[i].Length == 1) {
                this.layers[i] = new Layer(layers[i][0], 0, range);
            }
            if (layers[i].Length == 2) {
                this.layers[i] = new Layer(layers[i][0], layers[i][1], range);
            }
        }
    }

    /**
     * Y = WX + bias
     */
    public float[] Forward(float[] inputLayer) {
        if (this.layers.Length >= 2) {
			for (int i = 1; i < this.layers.Length ; i++) {
                this.layers[i].Feed(this.layers[i-1]); 
            }
        }
        return this.layers[this.layers.Length-1].weights;
    }

}
