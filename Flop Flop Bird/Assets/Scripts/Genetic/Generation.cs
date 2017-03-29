using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A Generation is a set of genomes.
 */
public class Generation {

	private List<Genome> genomes;

	// Build the generation
	public Generation() {
		genomes = new List<Genome> ();
	}

	// Add a genome to the generation
	public void AddGenome(Genome g) {
		genomes.Add (g);
	}

	// Breed 2 genomes to get children
	public List<Genome> BreedGenomes(Genome g1, Genome g2) {
		List<Genome> children = new List<Genome> ();

		for (int i = 0; i < GeneticManager.NB_CHILDREN; i++) {
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

		/*
		// Elitism
		for (int i = 0; i < GeneticManager.POPULATION * GeneticManager.ELITISM_FACTOR; i++) {
			if (next.genomes.Count < GeneticManager.POPULATION) {

			}
		}
		*/

		return next;
	}
}
