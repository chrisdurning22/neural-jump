using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
// using UnityEngine;
// using UnityEngine.UI;
// using Random = System.Random;

namespace neat
{
	public class Speciation {
		
		private const int GENE_THRESHOLD = 20;

		public static int excessCount(Genome g1, Genome g2) {
			int count = 0;
			int g1HighestInnovation = g1.getConnections().Keys.Max();
			int g2HighestInnovation = g2.getConnections().Keys.Max();

			// if g1 has lower innovation, count excess genes on g2
			if(g1HighestInnovation < g2HighestInnovation) {

				foreach(KeyValuePair<int, ConnectionGene> c in g2.getConnections()) {
					if(c.Key > g1HighestInnovation) {
						count++;
					}
				}
			} 
			else if(g1HighestInnovation > g2HighestInnovation) {

				foreach(KeyValuePair<int, ConnectionGene> c in g1.getConnections()) {
					if(c.Key > g2HighestInnovation) {
						count++;
					}
				}
			}
			return count;
		}

		public static int disjointCount(Genome g1, Genome g2) {
			int count = 0;
			int g1HighestInnovation = g1.getConnections().Keys.Max();
			int g2HighestInnovation = g2.getConnections().Keys.Max();

			if(g1HighestInnovation <= g2HighestInnovation) {

				foreach(KeyValuePair<int, ConnectionGene> c in g1.getConnections()) {
					if(!g2.getConnections().ContainsKey(c.Key)) {
						count++;
					}
				}

				foreach(KeyValuePair<int, ConnectionGene> c in g2.getConnections()) {
					if(!g1.getConnections().ContainsKey(c.Key) && c.Key < g1HighestInnovation) {
						count++;
					}
				}
			} 
			else if(g1HighestInnovation > g2HighestInnovation) {

				foreach(KeyValuePair<int, ConnectionGene> c in g2.getConnections()) {
					if(!g1.getConnections().ContainsKey(c.Key)) {
						count++;
					}
				}

				foreach(KeyValuePair<int, ConnectionGene> c in g1.getConnections()) {
					if(!g2.getConnections().ContainsKey(c.Key) && c.Key < g2HighestInnovation) {
						count++;
					}
				}
			}
			return count;
		}

		public static float averageWeightDifference(Genome g1, Genome g2) {
			float g1TotalWeights = 0.0f;
			float g2TotalWeights = 0.0f;
			float matchingGeneCounter = 0.0f;

			foreach(KeyValuePair<int, ConnectionGene> c in g1.getConnections()) {
				if(g2.getConnections().ContainsKey(c.Key)) {
					matchingGeneCounter++;
					g1TotalWeights += c.Value.getWeight();
				}
			}

			foreach(KeyValuePair<int, ConnectionGene> c in g2.getConnections()) {
				if(g1.getConnections().ContainsKey(c.Key)) {
					g2TotalWeights += c.Value.getWeight();
				}
			}	

			// Debug.Log("g1TotalWeights" + g1TotalWeights + "g2TotalWeights" + g1TotalWeights + "matchingGeneCounter" + matchingGeneCounter);
			//Console.WriteLine("g1TotalWeights " + g1TotalWeights + " g2TotalWeights " + g1TotalWeights + " matchingGeneCounter " + matchingGeneCounter);

			if(matchingGeneCounter == 0) {
				return 0f;
			}

			return (Math.Abs(g1TotalWeights - g2TotalWeights)) / matchingGeneCounter;
		}

		public static bool compatibilityDistance(Genome g1, Genome g2) {
			int normalize = 1;
			int g1GeneCount = g1.getConnections().Count;
			int g2GeneCount = g2.getConnections().Count;

			// if  either genome has more than 20 genes, change normalize
			if(g1GeneCount >= GENE_THRESHOLD || g2GeneCount >= GENE_THRESHOLD) {
				//normalise is set to the length of the largest genome
				normalize = Math.Max(g1GeneCount, g2GeneCount);
			}

			float cDistance =  ((excessCount(g1, g2) * Utility.COEFFICIENT1) / normalize)
					+ ((disjointCount(g1, g2) * Utility.COEFFICIENT2) / normalize)
					+ (averageWeightDifference(g1, g2) * Utility.COEFFICIENT3);
		
			return cDistance <= Utility.COMPATIBILITY_THRESHOLD;
		}
	}
}

