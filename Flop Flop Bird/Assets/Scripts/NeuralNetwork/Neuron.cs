﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron {

	public List<Synapse> inputs;
	public float threshold;
	public float currentWeight;

	public Neuron() {
		inputs = new List<Synapse>();
	}

	public void SetPreviousNeurons(List<Neuron> previousNeurons) {
		foreach (Neuron n in previousNeurons) {
			Synapse s = new Synapse ();
			s.input = n;
			inputs.Add (s);
		}
	}
}