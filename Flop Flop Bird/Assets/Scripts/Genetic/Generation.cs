using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A Generation is a set of genomes.
 */
public class Generation {

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

			children.Add (child);
		}

		return children;
	}

	// Compute the next generation
	public Generation NextGeneration() {
		Generation next = new Generation();
		next.RandomGeneration ();
		// TODO

		/*
		// Elitism
		for (int i = 0; i < POPULATION * ELITISM_FACTOR; i++) {
			if (next.genomes.Count < POPULATION) {

			}
		}
		*/

		// TODO : set the scores to 0

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
}
