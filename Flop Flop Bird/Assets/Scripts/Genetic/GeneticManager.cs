using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The GeneticManager handles the genetic algorithm.
 */
public class GeneticManager {

	// Some constants
	public static int POPULATION = 10;
	public static float ELITISM_FACTOR = .2f;
	public static float RANDOM_FACTOR = .2f;
	public static float MUTATION_RATE = .1f;
	public static float MUTATION_RANGE = .5f;
	public static int NB_CHILDREN = 2;
}
