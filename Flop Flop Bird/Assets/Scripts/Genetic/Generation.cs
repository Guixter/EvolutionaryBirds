using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A Generation is a set of genomes.
 */
public class Generation : IComparer<Genome> {

	// Some static constants
	public static int POPULATION = 100;
	public static float ELITISM_FACTOR = .2f;
	public static float RANDOM_FACTOR = .2f;
	public static float MUTATION_RATE = .1f;
	public static float MUTATION_RANGE = .5f;
	public static int NB_CHILDREN = 2;
	public static List<int> STRUCTURE;

	static Generation() {
		STRUCTURE = new List<int> ();
		STRUCTURE.Add (4);
		STRUCTURE.Add (4);
		STRUCTURE.Add (1);
	}

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
				if (Random.value <= .5f) {
					child.weights [j] = g2.weights [j];
				}

				// Perform some mutations
				if (Random.value <= MUTATION_RATE) {
					child.weights [j] += Random.Range (-1f, 1f) * MUTATION_RANGE;
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
				next.genomes.Add (Genome.RandomGenome(STRUCTURE));
			}
		}

		// Breed the current genomes
		for (int i = 0; i < POPULATION; i++) {
			for (int j = i + 1; j < POPULATION; j++) {
				List<Genome> children = BreedGenomes (genomes [i], genomes [j]);

				foreach (Genome g in children) {
					if (next.genomes.Count < POPULATION) {
						next.genomes.Add (g);
					}
				}

				if (next.genomes.Count < POPULATION) {
					break;
				}
			}
		}

		return next;
	}

	// Fill the generation with random genomes.
	public void RandomGeneration() {
		genomes.Clear ();
		for (int i = 0; i < POPULATION; i++) {
			genomes.Add (Genome.RandomGenome (STRUCTURE));
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
