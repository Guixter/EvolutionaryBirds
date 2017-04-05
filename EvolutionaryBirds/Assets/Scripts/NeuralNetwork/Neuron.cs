using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron {

	public List<Neuron> inputs;
	public float value;
	public float weight;

	public Neuron() {
		inputs = new List<Neuron>();
	}

	public void SetPreviousNeurons(List<Neuron> previousNeurons) {
		foreach (Neuron n in previousNeurons) {
			inputs.Add (n);
		}
	}
}