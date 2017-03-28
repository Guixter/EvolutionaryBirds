using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedForward {

    private static class Layer {
        private double[] biases;
        private double[] weights;
        /**
         *
         */
        public Layer(int weights, int biases, int[] range) {
            this.biases = new double[biases];
            this.weights = new double[weights];
            Random rand = new Random(Environment.TickCount);
            for (int i = 0; i < weights; i++) {
                if (range.length == 1) {
                    this.weights[i] = rand.Next(range[0]) * .1d;
                } else if (range.length == 2) {
                    this.weights[i] = rand.Next(range[0], range[1]) * .1d;
                } else {
                    this.weights[i] = rand.Next(-1.0, 1.0) * .1d;
                }
            }
            for (int i = 0; i < biases; i++) {
                if (range.length == 1) {
                    this.biases[i] = rand.Next(range[0]) * .1d;
                } else if (range.length == 2) {
                    this.biases[i] = rand.Next(range[0], range[1]) * .1d;
                } else {
                    this.biases[i] = rand.Next(-1.0, 1.0) * .1d;
                }
            }
        }

        public static double Sigmoid(double x) {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        public void Feed(Layer previous) {
            double prev = 0.0;
            for (int i = 0; i < previous.weights.length; i++) {
                prev += previous.weights[i];
            }
            prev = Sigmoid(prev);
            for (int i = 0; i < weights.length; i++) {
                this.weights[i] += prev;
            }
        }
    }

    private Layer[] layers;
    private double LearningRate;

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
    public FeedForward(int[,] layers, int[] range) {
        for (int i = 0; i < layers.length; i++) {
            if (layers[i].length == 1) {
                this.layers[i] = new Layer(layers[i][0], 0, range);
            }
            if (layers[i].length == 2) {
                this.layers[i] = new Layer(layers[i][0], layers[i][1], range);
            }
        }
    }

    /**
     * Y = WX + bias
     */
    public double[] Forward(double[] inputLayer) {
        if (this.layers.length >= 2) {
            for (int i = 1; i < this.layers.length) {
                this.layers[i].Feed(this.layers[i-1]); 
            }
        }
        return this.layers[this.layers.length-1];
    }

}
