using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

using System;
using Random = System.Random;

namespace neat
{
	public class GenomeIntegrationTests {

	Genome genome;
	Random random;

	[SetUp]
	public void init() {
			random = new Random();
			genome = new Genome();

			int node0 = genome.getNodes().Count;
			genome.addNodeGene(new NodeGene(NodeGene.NodeType.INPUT, node0));
			int node1 = genome.getNodes().Count;
			genome.addNodeGene(new NodeGene(NodeGene.NodeType.INPUT, node1));
			int node2 = genome.getNodes().Count;
			genome.addNodeGene(new NodeGene(NodeGene.NodeType.HIDDEN, node2));
			int node3 = genome.getNodes().Count;
			genome.addNodeGene(new NodeGene(NodeGene.NodeType.HIDDEN, node3));
			int node4 = genome.getNodes().Count;
			genome.addNodeGene(new NodeGene(NodeGene.NodeType.OUTPUT, node4));

			InnovationUtility.incrementGlobalInnovation();
			genome.addConnectionGene(new ConnectionGene(node0, node2, -0.9f, true, InnovationUtility.getGlobalInnovation()));
			InnovationUtility.incrementGlobalInnovation();
			genome.addConnectionGene(new ConnectionGene(node1, node2, 0.9f, true, InnovationUtility.getGlobalInnovation()));
			InnovationUtility.incrementGlobalInnovation();
			genome.addConnectionGene(new ConnectionGene(node1, node3, -0.1f, true, InnovationUtility.getGlobalInnovation()));
			InnovationUtility.incrementGlobalInnovation();
			genome.addConnectionGene(new ConnectionGene(node2, node3, 0.1f, true, InnovationUtility.getGlobalInnovation()));
			InnovationUtility.incrementGlobalInnovation();
			genome.addConnectionGene(new ConnectionGene(node3, node4, 0.2f, true, InnovationUtility.getGlobalInnovation()));

	}

	[Test]
		public void Length_After_Connection_Mutation() {
			genome.addConnectionMutation(random);
			Assert.AreEqual(6, genome.getConnections().Count);
		}

		[Test]
		public void Innovation_After_Connection_Mutation() {
			genome.addConnectionMutation(random);
			Assert.AreEqual(6, genome.getConnections()[genome.getInnovationNumbers()[genome.getInnovationNumbers().Count - 1]].getInnovationNumber());
		}

		[Test]
		public void Weight_After_Connection_Mutation() {
			genome.addConnectionMutation(random);
			
			if(genome.getConnections()[genome.getInnovationNumbers()[genome.getInnovationNumbers().Count - 1]].getWeight() <= 1.0 &&
				genome.getConnections()[genome.getInnovationNumbers()[genome.getInnovationNumbers().Count - 1]].getWeight() >= -1.0) {
					Assert.Pass();
			}
			Assert.Fail();
		}

		[Test]
		public void Connections_Length_After_Node_Mutation() {
			genome.addNodeMutation(random);
			Assert.AreEqual(7, genome.getConnections().Count);
		}

		[Test]
		public void Nodes_Length_After_Node_Mutation() {
			genome.addNodeMutation(random);
			Assert.AreEqual(6, genome.getNodes().Count);
		}

		[Test]
		public void Connection_Weight_After_Node_Mutation() {
			genome.addNodeMutation(random);
			Assert.AreEqual(1f, genome.getConnections()[genome.getInnovationNumbers()[genome.getInnovationNumbers().Count - 2]].getWeight());
		}

		[Test]
		public void Split_Weight_Equals_Last_Gene_Weight_After_Node_Mutation() {
			genome.addNodeMutation(random);
			foreach(KeyValuePair<int, ConnectionGene> gene in genome.getConnections()) {

				// first          third
				//  in   out  in   out
				//  []  ->  []  ->  []
				// if the in from the first node and the out from the third node are the in and out of the current gene (the split node)
				if(genome.getConnections()[genome.getInnovationNumbers()[genome.getInnovationNumbers().Count - 1]].getOut() == gene.Value.getIn() &&
					genome.getConnections()[genome.getInnovationNumbers()[genome.getInnovationNumbers().Count - 2]].getIn() == gene.Value.getIn()) {
						Assert.AreEqual(gene.Value.getWeight(), genome.getConnections()[genome.getInnovationNumbers()[genome.getInnovationNumbers().Count - 2]].getWeight());
				}
			}
		}

		[Test]
		public void Split_Node_Is_Disabled_After_Node_Mutation() {
			genome.addNodeMutation(random);
			foreach(KeyValuePair<int, ConnectionGene> gene in genome.getConnections()) {

				if(genome.getConnections()[genome.getInnovationNumbers()[genome.getInnovationNumbers().Count - 1]].getOut() == gene.Value.getIn() &&
					genome.getConnections()[genome.getInnovationNumbers()[genome.getInnovationNumbers().Count - 2]].getIn() == gene.Value.getIn()) {
						Assert.AreEqual(false, gene.Value.getEnabled());
				}
			}
		}

		[Test]
		public void Innovation_Last_After_Node_Mutation() {
			genome.addNodeMutation(random);
			Assert.AreEqual(7, genome.getConnections()[genome.getInnovationNumbers()[genome.getInnovationNumbers().Count - 1]].getInnovationNumber());
		}

		[Test]
		public void Innovation_Second_Last_After_Node_Mutation() {
			genome.addNodeMutation(random);
			Assert.AreEqual(6, genome.getConnections()[genome.getInnovationNumbers()[genome.getInnovationNumbers().Count - 2]].getInnovationNumber());
		}

		[Test]
		public void Check_Calculate_Output() {

			float node2 = ActivationFunction.sigmoid(1f * -0.9f) + ActivationFunction.sigmoid(1f * 0.9f);
			float node3 = ActivationFunction.sigmoid(node2 * 0.1f) + ActivationFunction.sigmoid(1f * -0.1f);
			float expectedOutput = ActivationFunction.sigmoid(node3 * 0.2f);

			float actualOutput = genome.calculateOutput(1f, 1f);
			Assert.AreEqual(expectedOutput, actualOutput);
		}
	}
}

