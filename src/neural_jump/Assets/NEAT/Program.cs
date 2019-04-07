using System;
using System.Collections.Generic;
using System.Configuration;



namespace neat {
	public class Program {

	private static List<Genome> bestGen;

		public static void Main (string[] args) {

		
			Random random = new Random();

			
			Genome initialGenome = new Genome();

			int node0 = initialGenome.getNodes().Count;
			initialGenome.addNodeGene(new NodeGene(NodeGene.NodeType.INPUT, node0));
			int node1 = initialGenome.getNodes().Count;
			initialGenome.addNodeGene(new NodeGene(NodeGene.NodeType.INPUT, node1));
			int node2 = initialGenome.getNodes().Count;
			initialGenome.addNodeGene(new NodeGene(NodeGene.NodeType.OUTPUT, node2));

			InnovationUtility.incrementGlobalInnovation();
			initialGenome.addConnectionGene(new ConnectionGene(node0, node2, Genome.getRandomFloatBetweenPoints(random, -1.0, 1.0), true, InnovationUtility.getGlobalInnovation()));

			InnovationUtility.incrementGlobalInnovation();
			initialGenome.addConnectionGene(new ConnectionGene(node1, node2, Genome.getRandomFloatBetweenPoints(random, -1.0, 1.0), true, InnovationUtility.getGlobalInnovation()));

			Genome initialGenome1 = new Genome();

			node0 = initialGenome1.getNodes().Count;
			initialGenome1.addNodeGene(new NodeGene(NodeGene.NodeType.INPUT, node0));
			node1 = initialGenome1.getNodes().Count;
			initialGenome1.addNodeGene(new NodeGene(NodeGene.NodeType.INPUT, node1));
			node2 = initialGenome1.getNodes().Count;
			initialGenome1.addNodeGene(new NodeGene(NodeGene.NodeType.OUTPUT, node2));

			initialGenome1.addConnectionGene(new ConnectionGene(node0, node2, Genome.getRandomFloatBetweenPoints(random, -1.0, 1.0), true, 1));

			initialGenome1.addConnectionGene(new ConnectionGene(node1, node2, Genome.getRandomFloatBetweenPoints(random, -1.0, 1.0), true, 2));

			Genome initialGenome2 = new Genome();

			node0 = initialGenome2.getNodes().Count;
			initialGenome2.addNodeGene(new NodeGene(NodeGene.NodeType.INPUT, node0));
			node1 = initialGenome2.getNodes().Count;
			initialGenome2.addNodeGene(new NodeGene(NodeGene.NodeType.INPUT, node1));
			node2 = initialGenome2.getNodes().Count;
			initialGenome2.addNodeGene(new NodeGene(NodeGene.NodeType.OUTPUT, node2));

			initialGenome2.addConnectionGene(new ConnectionGene(node0, node2, Genome.getRandomFloatBetweenPoints(random, -1.0, 1.0), true, 1));

			initialGenome2.addConnectionGene(new ConnectionGene(node1, node2, Genome.getRandomFloatBetweenPoints(random, -1.0, 1.0), true, 2));

			Genome initialGenome3 = new Genome();

			node0 = initialGenome3.getNodes().Count;
			initialGenome3.addNodeGene(new NodeGene(NodeGene.NodeType.INPUT, node0));
			node1 = initialGenome3.getNodes().Count;
			initialGenome3.addNodeGene(new NodeGene(NodeGene.NodeType.INPUT, node1));
			node2 = initialGenome3.getNodes().Count;
			initialGenome3.addNodeGene(new NodeGene(NodeGene.NodeType.OUTPUT, node2));

			initialGenome3.addConnectionGene(new ConnectionGene(node0, node2, Genome.getRandomFloatBetweenPoints(random, -1.0, 1.0), true, 1));

			initialGenome3.addConnectionGene(new ConnectionGene(node1, node2, Genome.getRandomFloatBetweenPoints(random, -1.0, 1.0), true, 2));

			
			// //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

			// Genome initialGenome2 = new Genome();

			// node1 = initialGenome2.getNodes().Count;
			// initialGenome2.addNodeGene(new NodeGene(NodeGene.NodeType.INPUT, node1));
			// node2 = initialGenome2.getNodes().Count;
			// initialGenome2.addNodeGene(new NodeGene(NodeGene.NodeType.INPUT, node2));
			// node3 = initialGenome2.getNodes().Count;
			// initialGenome2.addNodeGene(new NodeGene(NodeGene.NodeType.OUTPUT, node3));
			
			// InnovationUtility.incrementGlobalInnovation();
			// initialGenome2.addConnectionGene(new ConnectionGene(node1, node3, Genome.getRandomFloatBetweenPoints(random, -1.0, 1.0), true, 1));

			// InnovationUtility.incrementGlobalInnovation();
			// initialGenome2.addConnectionGene(new ConnectionGene(node2, node3, Genome.getRandomFloatBetweenPoints(random, -1.0, 1.0), true, 2));


//-------------------------------------------------------------------

			initialGenome.addNodeMutation(random);
			initialGenome3.addNodeMutation(random);
			

			Console.Write("initialGenome: ");
			foreach (KeyValuePair<int, ConnectionGene> c in initialGenome.getConnections()) {
				Console.Write("[" + c.Key + "]");
			}

			Console.Write("\n" + "initialGenome1: ");
			foreach (KeyValuePair<int, ConnectionGene> c in initialGenome1.getConnections()) {
				Console.Write("[" + c.Key + "]");
			}
			Console.Write("\n");

			Console.WriteLine("disjoint: " + Speciation.disjointCount(initialGenome, initialGenome1));
			Console.WriteLine("excess: " + Speciation.excessCount(initialGenome, initialGenome1));

			Console.Write("\n");			

			GeneratePopulation population = new GeneratePopulation(4);

			population.addGenomeToPopulation(new Genome(initialGenome));
			population.addGenomeToPopulation(new Genome(initialGenome1));
			population.addGenomeToPopulation(new Genome(initialGenome2));
			population.addGenomeToPopulation(new Genome(initialGenome3));

			for(int i = 0; i < 60; i++) {
				population.sortGenomesIntoSpecies(random);
				population.addAdjustedFitness();
				population.fillNextGeneration(random);
				Console.WriteLine("\n Generation: " + i);
				// foreach(Species s in population.getSpeciesList()) {
				// 	Console.WriteLine("New Species ");
				// 	foreach(Genome g in s.getGenomesList()) {
				// 		Console.WriteLine("Genome fitness: " + g.getFitness());
				// 	}
				// }
			}


			

//--------------------------------------------------------------------------------


			// Console.WriteLine("Parent1");
			// foreach(KeyValuePair<int, NodeGene> n in initialGenome.getNodes()) {
			// 	Console.WriteLine("ID: " + n.Value.getNodeNumber() + " Layer: " + n.Value.getNodeType());
			// }
			
			// Console.Write("\n");

			// foreach(KeyValuePair<int, ConnectionGene> c in initialGenome.getConnections()) {
			// 	Console.WriteLine("In: " + c.Value.getIn() + " Out: " + c.Value.getOut() + " Innov: " + c.Value.getInnovationNumber() + " Enabled: " + c.Value.getEnabled() + " Weight: " + c.Value.getWeight());
			// }

			// Console.Write("\n");

			// Console.WriteLine("Parent2");
			// foreach(KeyValuePair<int, NodeGene> n in initialGenome2.getNodes()) {
			// 	Console.WriteLine("ID: " + n.Value.getNodeNumber() + " Layer: " + n.Value.getNodeType());
			// }
			
			// Console.Write("\n");

			// foreach(KeyValuePair<int, ConnectionGene> c in initialGenome2.getConnections()) {
			// 	Console.WriteLine("In: " + c.Value.getIn() + " Out: " + c.Value.getOut() + " Innov: " + c.Value.getInnovationNumber() + " Enabled: " + c.Value.getEnabled() + " Weight: " + c.Value.getWeight());
			// }

			// Console.Write("\n");

			// Genome child = Evolution.crossover(random, initialGenome, initialGenome2);

			// Console.WriteLine("Child");
			// foreach(KeyValuePair<int, NodeGene> n in child.getNodes()) {
			// 	Console.WriteLine("ID: " + n.Value.getNodeNumber() + " Layer: " + n.Value.getNodeType());
			// }
			
			// Console.Write("\n");

			// foreach(KeyValuePair<int, ConnectionGene> c in child.getConnections()) {
			// 	Console.WriteLine("In: " + c.Value.getIn() + " Out: " + c.Value.getOut() + " Innov: " + c.Value.getInnovationNumber() + " Enabled: " + c.Value.getEnabled() + " Weight: " + c.Value.getWeight());
			// }


			//------------------------------------------------------------------------------------------------------------------------------------------------------------
			
			// Console.WriteLine(Speciation.compatibilityDistance(initialGenome, initialGenome2));


			//------------------------------------------------------------------------------------------------------------------------------------------------------------

			// GeneratePopulation population = new GeneratePopulation(4);

			// population.addGenomeToPopulation(new Genome(initialGenome));
			// population.addGenomeToPopulation(new Genome(initialGenome1));
			// population.addGenomeToPopulation(new Genome(initialGenome2));
			// population.addGenomeToPopulation(new Genome(initialGenome3));
			
	
			// GeneratePopulation population = new GeneratePopulation(50);

			// for(int i = 0; i < population.getPopulationSize(); i++) {
			// 	population.addGenomeToPopulation(new Genome(initialGenome));
			// }

	
			// print connection genes
			// Console.WriteLine("CONNECTION GENES: ");
			// foreach(ConnectionGene gene in initialGenome.connections) {
			// 	Console.WriteLine("IN: " + gene.getIn() + " OUT: " + gene.getOut() + " WEIGHT: " + gene.getWeight() + " ENABLED: " + gene.getEnabled() + " INNO: " + gene.getInnovationNumber());
				
			// }

			// Console.WriteLine("\n");

						


			// for(int i = 1; i < 20; i++) {
			// 	bestGen = population.GenerateNextGeneration(random);


			// 	Console.Write(" Generation: " + i);
			// 	// Console.Write(", Species Count: " + population.getSpeciesList().Count);
			// 	Console.Write(", Highest Fitness: " + population.getHighestScore() + "\n");
			// }


		}
	
	}
}
