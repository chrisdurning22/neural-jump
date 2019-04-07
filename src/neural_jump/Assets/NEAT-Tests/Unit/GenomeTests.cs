using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using Random = System.Random;

namespace  neat
{
	
	public class GenomeTests {

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
		public void Check_NodeType() {
			Assert.AreEqual(NodeGene.NodeType.INPUT, genome.getNodes()[genome.getNodeNumbers()[0]].getNodeType());
		}

		[Test]
		public void Check_NodeNumber() {
			Assert.AreEqual(0, genome.getNodeNumbers()[0]);
		}

		[Test]
		public void Check_Connection_Weights() {
			Assert.AreEqual(-0.9f, genome.getConnections()[genome.getInnovationNumbers()[0]].getWeight());
		}

		[Test]
		public void Check_Innovation_Number() {
			Assert.AreEqual(3, genome.getConnections()[genome.getInnovationNumbers()[2]].getInnovationNumber());
		}

		[Test]
		public void Check_Input_Node() {
			Assert.AreEqual(0, genome.getConnections()[genome.getInnovationNumbers()[0]].getIn());
		}

		[Test]
		public void Check_Output_Node() {
			Assert.AreEqual(2, genome.getConnections()[genome.getInnovationNumbers()[0]].getOut());
		}

		[Test]
		public void Make_Node_One_Point_To_Node_Two() {
			NodeGene n1 = new NodeGene(NodeGene.NodeType.HIDDEN, 0);
			NodeGene n2 = new NodeGene(NodeGene.NodeType.INPUT, 1);			
			genome.makeN1PointToN2(ref n1, ref n2);
			Assert.AreEqual(NodeGene.NodeType.HIDDEN, n2.getNodeType());

		}

		[Test]
		public void Get_Different_Node_From_Nodes() {
			NodeGene n1 = genome.getNodes()[3];
		
			if(genome.getDifferentNode(random, n1, genome.getNodes()) != n1 && genome.getDifferentNode(random, n1, genome.getNodes()) != null) {
			 	Assert.Pass();
			}
			Assert.Fail();
		}

		[Test]
		public void All_Connections_Made() {
			if(!genome.allConnectionsMade()) {
				Assert.Pass();
			}
			Assert.Fail();
		}

		[Test]
		public void All_Connections_Made_After_All_Connections_Have_Been_Added() {
			genome.addConnectionMutation(random);
			genome.addConnectionMutation(random);
			genome.addConnectionMutation(random);

			if(genome.allConnectionsMade()) {
				Assert.Pass();
			}
			Assert.Fail();
		}

		[Test]
		public void Check_Get_Random_Between_Points() {
			if(Genome.getRandomFloatBetweenPoints(random, -2.0, 2.0) <= 2.0 &&
				Genome.getRandomFloatBetweenPoints(random, -2.0, 2.0) >= -2.0 &&
				Genome.getRandomFloatBetweenPoints(random, -0.5, 0.5) <= 0.5 &&
				Genome.getRandomFloatBetweenPoints(random, -0.5, 0.5) >= -0.5) {
					Assert.Pass();
				}
				Assert.Fail();
		}

		[Test]
		public void Check_Perturbed() {
			genome.perturbedOrAssignedNewValue(random);
			// -0.9f is weight given to first gene
			if(genome.getConnections()[genome.getInnovationNumbers()[0]].getWeight() != -0.9f) {
				Assert.Pass();
			}
			Assert.Fail();
		}
	}		
}

