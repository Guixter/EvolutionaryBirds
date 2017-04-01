using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A Generation is a set of genomes.
 */
public class Generation : IComparer<Genome> {

	// Some static constants
	public static int POPULATION = 10;
	public static float ELITISM_FACTOR = .2f;
	public static float RANDOM_FACTOR = .2f;
	public static float MUTATION_RATE = .1f;
	public static float MUTATION_RANGE = .5f;
	public static int NB_CHILDREN = 2;

	// Properties;
	public List<Genome> genomes { get; set; }

	////////////////////////////////////////////////////////////////

	// Build the generation
	public Generation() {
		genomes = new List<Genome> ();
	}

	// Breed 2 genomes to get children
	public List<Genome> BreedGenomes(Genome g1, Genome g2) {
		List<Genome> children = new List<Genome> ();

		for (int i = 0; i < NB_CHILDREN; i++) {
			Genome child = g1.Clone ();

			for (int j = 0; j < child.weights.Count; j++) {
				if (Random.value > .5f) {
					child.weights [j] = g2.weights [j];
				}
			}
			for (int j = 0; j < child.thresholds.Count; j++) {
				if (Random.value > .5f) {
					child.thresholds [j] = g2.thresholds [j];
				}
			}

			children.Add (child);
		}

		return children;
	}

	// Compute the next generation
	public Generation NextGeneration() {
		Generation next = new Generation();

		// Sort the genomes
		genomes.Sort(this);
		genomes.Reverse ();

		// Elitism
		for (int i = 0; i < POPULATION * ELITISM_FACTOR; i++) {
			if (next.genomes.Count < POPULATION) {
				next.genomes.Add (genomes[i].Clone());
			}
		}

		// Random
		for (int i = 0; i < POPULATION * RANDOM_FACTOR; i++) {
			if (next.genomes.Count < POPULATION) {
				Genome g = new Genome ();
				next.genomes.Add (g);
			}
		}

		return next;
	}

	// Fill the generation with random genomes.
	public void RandomGeneration() {
		genomes.Clear ();
		for (int i = 0; i < POPULATION; i++) {
			Genome g = new Genome ();
			g.neuronsPerLayer.Add (2);
			g.neuronsPerLayer.Add (2);
			g.neuronsPerLayer.Add (1);
			for (int j = 0; j < 5; j++) {
				g.weights.Add (Random.value * 2 - 1);
			}
			genomes.Add (g);
		}
	}

	// Comparison of 2 genomes
	public int Compare (Genome x, Genome y) {
		if (x.fitness > y.fitness) {
			return 1;
		} else if (x.fitness == y.fitness) {
			return 0;
		} else {
			return -1;
		}
	}
}
