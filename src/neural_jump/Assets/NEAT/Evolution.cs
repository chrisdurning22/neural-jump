using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace neat
{
	public class Evolution {

		// matching genes are inherited randomly, disjoint and excess (unmatching genes) genes are inherited from the more fit parent
		// parent1 is the more fit parent
		public static Genome crossover(Random random, Genome parent1, Genome parent2) {

			Genome offspring = new Genome();

            /**
             * NodeGene Crossover
             */

            // add NodeGenes from parent1 (fittest parent) to the offspring
            foreach (KeyValuePair<int, NodeGene> n in parent1.getNodes()) {

                NodeGene oNode = new NodeGene(n.Value);
                offspring.addNodeGene(oNode); 
            }

            /**
             * ConnectionGene Crossover
             */
            
			foreach(KeyValuePair<int, ConnectionGene> p1Gene in parent1.getConnections()) {

                // when parent connection genes match one must be inherited by the offspring randomly
				if(parent2.getConnections().ContainsKey(p1Gene.Key)) {

                    if(random.Next(100) <= 50) {

                        // takes a copy of parent2 gene
                        offspring.addConnectionGene(new ConnectionGene(parent2.getConnections()[p1Gene.Key]));
                    } 
                    else {
                        //takes a copy of parent1 gene
                        offspring.addConnectionGene(new ConnectionGene(p1Gene.Value));
                    }
				}
                else { // if they don't match (disjoint or excess), copy from more fit parent

                    offspring.addConnectionGene(new ConnectionGene(p1Gene.Value));
                }

			}

			return offspring;
		}
	}
}

