using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A Generation is a set of genomes.
 */
public class Generation : IComparer<Genome> {

	// Some static constants
	public static int POPULATION = 50;
	public static float ELITISM_FACTOR = .2f;
	public static float RANDOM_FACTOR = .1f;
	public static float MUTATION_RATE = .1f;
	public static float MUTATION_RANGE = .5f;
	public static int NB_CHILDREN = 1;
	public static List<int> STRUCTURE;

	static Generation() {
		STRUCTURE = new List<int> ();
		STRUCTURE.Add (2);
		STRUCTURE.Add (2);
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

			if (Random.value <= .5f) {
				child.threshold = g2.threshold;
			}
			if (Random.value <= MUTATION_RATE) {
				child.threshold += Random.Range (-1f, 1f) * MUTATION_RANGE;
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

		// Get the total fitness
		float totalFitness = 0;
		for (int i = 0; i < POPULATION; i++) {
			totalFitness += genomes [i].fitness;
		}

		// Breed some genomes
		while (next.genomes.Count < POPULATION) {
			float fit1 = Random.value * totalFitness;
			float fit2 = Random.value * totalFitness;
			Genome parent1 = null, parent2 = null;

			float currentFit = 0;

			for (int i = 0; i < POPULATION; i++) {

				if (parent1 == null && fit1 <= currentFit + genomes[i].fitness) {
					parent1 = genomes [i];
				}

				if (parent2 == null && fit2 <= currentFit + genomes[i].fitness) {
					parent2 = genomes [i];
				}

				if (parent1 != null && parent2 != null) {
					break;
				}

				currentFit += genomes [i].fitness;
			}

			List<Genome> children = BreedGenomes (parent1, parent2);

			foreach (Genome g in children) {
				if (next.genomes.Count < POPULATION) {
					next.genomes.Add (g);
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
