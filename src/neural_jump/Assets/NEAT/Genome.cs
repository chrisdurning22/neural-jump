using System;
using System.Collections.Generic;
using System.Configuration;

namespace neat
{
	public class Genome {
		private float jumpsMade;
		private float distanceTravelled;
		private float fitness;
		private int perturbedPercentage = 90;

		private Dictionary<int, ConnectionGene> connections;
		private Dictionary<int, NodeGene> nodes;
		private List<int> innovationNumbers;
		private List<int> nodeNumbers;

		public Genome() {

			connections = new Dictionary<int, ConnectionGene>();
			nodes = new Dictionary<int, NodeGene>();
			innovationNumbers = new List<int>();
			nodeNumbers = new List<int>();
			fitness = 0f;
		}

		public Genome(Genome copy) {
			connections = new Dictionary<int, ConnectionGene>();
			nodes = new Dictionary<int, NodeGene>();
			innovationNumbers = new List<int>();
			nodeNumbers = new List<int>();
			fitness = 0f;

			foreach(KeyValuePair<int, NodeGene> n in copy.nodes) {
				nodes.Add(n.Key, new NodeGene(n.Value));
				nodeNumbers.Add(n.Key);
			}

			foreach(KeyValuePair<int, ConnectionGene> c in copy.connections) {
				connections.Add(c.Key, new ConnectionGene(c.Value));
				innovationNumbers.Add(c.Key);
			}
		}

		public void setDistanceTravelled(float distanceTravelled) {
			this.distanceTravelled = distanceTravelled;
		}

		public float getDistanceTravelled() {
			return this.distanceTravelled;
		}

		public void addToJumpsMade() {
			this.jumpsMade += 1;
		}

		public void setJumpsMade(float jumpsMade) {
			this.jumpsMade = jumpsMade;
		}

		public float getJumpsMade() {
			return this.jumpsMade;
		}

		public float getDistanceMinusJumps() {
			return getDistanceTravelled() - getJumpsMade();
		}

		public void setFitness(float fitness) {
			this.fitness = fitness;
		}

		public float getFitness() {
			return this.fitness;
		}

		public Dictionary<int, ConnectionGene> getConnections() {
			return connections;
		}

		public Dictionary<int, NodeGene> getNodes() {
			return nodes;
		}

		public List<int> getNodeNumbers() {
			return nodeNumbers;
		}

		public List<int> getInnovationNumbers() {
			return innovationNumbers;
		}

		public static float getRandomFloatBetweenPoints(Random random, double minimum, double maximum) {
			return (float)(((random.NextDouble () * (maximum - (minimum)) + (minimum))));
		}

		public void makeN1PointToN2(ref NodeGene n1, ref NodeGene n2) {
			if (n1.getNodeType() == NodeGene.NodeType.HIDDEN && n2.getNodeType() == NodeGene.NodeType.INPUT ||
				n1.getNodeType() == NodeGene.NodeType.OUTPUT && n2.getNodeType() == NodeGene.NodeType.INPUT ||
				n1.getNodeType() == NodeGene.NodeType.OUTPUT && n2.getNodeType() == NodeGene.NodeType.HIDDEN) 
			{
				NodeGene temp = n1;
				n1 = n2;
				n2 = temp;
			}
		}

		// ensures n1 and n2 are different
		public NodeGene getDifferentNode(Random random, NodeGene n1, Dictionary<int, NodeGene> nodes) {
			NodeGene n2 = nodes[nodeNumbers[random.Next(nodeNumbers.Count)]];
		
			while (n2.getNodeNumber() == n1.getNodeNumber()) {
				n2 = nodes[nodeNumbers[random.Next(nodeNumbers.Count)]];
			}

			return n2;
		}

		// if all connections have already been made a new connection cannot be added
		public bool allConnectionsMade() {
			int inputCount = 0;
			int hiddenCount = 0;
			int outputCount = 0;

			foreach (KeyValuePair<int, NodeGene> n in nodes) {
				if(n.Value.getNodeType() == NodeGene.NodeType.INPUT) {
					inputCount++;
				} else if(n.Value.getNodeType() == NodeGene.NodeType.HIDDEN) {
					hiddenCount++;
				} else if(n.Value.getNodeType() == NodeGene.NodeType.OUTPUT) {
					outputCount++;
				}
			}

			// maxConnections = the number of possible connections
			int maxConnections = (inputCount * hiddenCount) + (hiddenCount * outputCount) + (inputCount * outputCount);
			if(maxConnections == connections.Count) {
				return true;
			}

			return false;
		}

		public bool addConnectionMutation(Random random) {

			bool uniqueMatch = false;
			NodeGene n1 = null;
			NodeGene n2 = null;
	
			if(allConnectionsMade()) {
				return false;
			}

			// to be used in circular loop checker
			Utility.nodeConnections.Clear();
			foreach (KeyValuePair<int, ConnectionGene> c in connections) {
				//if key already exists add to hashset
				if(Utility.nodeConnections.ContainsKey(c.Value.getOut())) {
					Utility.nodeConnections[c.Value.getOut()].Add(c.Value.getIn());
				} 
				else {
					Utility.nodeConnections.Add(c.Value.getOut(), new HashSet<int>(){c.Value.getIn()});
				}
			}

			while(!uniqueMatch) {
				uniqueMatch = true;
				n1 = nodes[nodeNumbers[random.Next(nodeNumbers.Count)]];
				n2 = getDifferentNode(random, n1, nodes);

				if(circularConnectionFound(n1.getNodeNumber(), n2.getNodeNumber())) {
					return false;
				}
				
				// if n1 and n1 aren't INPUT nodes
				if(!(n1.getNodeType() == NodeGene.NodeType.INPUT && n2.getNodeType() == NodeGene.NodeType.INPUT)) {
					makeN1PointToN2 (ref n1, ref n2);
					
					foreach(KeyValuePair<int, ConnectionGene> c in connections) {
						// checks to see if connection already exists
						if (c.Value.getIn() == n1.getNodeNumber() && c.Value.getOut() == n2.getNodeNumber()) {
							uniqueMatch = false;
						} 
					}
				} 
				else {
					uniqueMatch = false;
				}
			}
			
			InnovationUtility.incrementGlobalInnovation();
			addConnectionGene(new ConnectionGene(n1.getNodeNumber(), n2.getNodeNumber(), getRandomFloatBetweenPoints(random, -1.0, 1.0), true, InnovationUtility.getGlobalInnovation()));
			
			return true;
		}

		private bool circularConnectionFound(int nodeNumber1, int nodeNumber2) {
			if(!Utility.nodeConnections.ContainsKey(nodeNumber1)) {
				return false;	
			}
			if(nodeNumber1 == nodeNumber2) {
				return true;
			}
			else {
				HashSet<int> inputs = Utility.nodeConnections[nodeNumber1];

				foreach(int inValue in inputs) {
					return circularConnectionFound(inValue, nodeNumber2);
				}
				return false;
			}
		}

		public void addNodeMutation(Random random) {

			// pick a random index
			int splitGeneIndex = innovationNumbers[random.Next(innovationNumbers.Count)];

			ConnectionGene c = connections[splitGeneIndex];
			c.setEnabled (false);

			NodeGene n1 = nodes[c.getIn()];
			NodeGene n2 = nodes[c.getOut()];

			// new node
			NodeGene n3 = new NodeGene(NodeGene.NodeType.HIDDEN, nodes.Count);

			// add connection genes (1) leading into the new node gets a weight of 1 (2) leading out of new node gets the same weight as old connection
			InnovationUtility.incrementGlobalInnovation();      
			addConnectionGene(new ConnectionGene(n1.getNodeNumber(), n3.getNodeNumber(), 1f, true, InnovationUtility.getGlobalInnovation()));

			InnovationUtility.incrementGlobalInnovation();
			addConnectionGene(new ConnectionGene(n3.getNodeNumber(), n2.getNodeNumber(), c.getWeight(), true, InnovationUtility.getGlobalInnovation()));
			connections [splitGeneIndex] = c;

			// add node gene 
			addNodeGene(n3);

		}

		public void addConnectionGene(ConnectionGene gene) {
			connections.Add(gene.getInnovationNumber(), gene);
			addInnovationNumber(gene.getInnovationNumber());
		}
		
		public void addNodeGene(NodeGene node) {
			nodes.Add(node.getNodeNumber(), node);
			addNodeNumber(node.getNodeNumber());
		}

		public void addNodeNumber(int nodeNumber) {
			nodeNumbers.Add(nodeNumber);
		}

		public void addInnovationNumber(int innovationNumber) {
			innovationNumbers.Add(innovationNumber);
		}

		public void perturbedOrAssignedNewValue(Random random) {
			// 90% perturbed by random value, 10% assigned new random value
			foreach (KeyValuePair<int, ConnectionGene> c in connections) {
				if(random.Next(0, 100) < perturbedPercentage) {
					c.Value.setWeight(getRandomFloatBetweenPoints(random, Utility.RANDOM_VAL_MIN, Utility.RANDOM_VAL_MIN) * c.Value.getWeight());
				} else {
					c.Value.setWeight(getRandomFloatBetweenPoints(random, Utility.RANDOM_VAL_MAX, Utility.RANDOM_VAL_MAX));
				}
			}
		}

		public float calculateOutput(float inputDistance, float inputHeight) {
			float nodeSum = 0f;
		
			foreach(KeyValuePair<int, NodeGene> n in nodes) {
				if(n.Value.getNodeNumber() == 0) {
					n.Value.setNodeValue(inputDistance);
				}

				if(n.Value.getNodeNumber() == 1) {
					n.Value.setNodeValue(inputHeight);
				}

				if(n.Value.getNodeType() != NodeGene.NodeType.INPUT) {
					foreach(KeyValuePair<int, ConnectionGene> c in connections) {
						if(c.Value.getOut() == n.Value.getNodeNumber() && c.Value.getEnabled()) {
							nodeSum += nodes[c.Value.getIn()].getNodeValue() * c.Value.getWeight();
							
						}
					}
					n.Value.setNodeValue(ActivationFunction.sigmoid(nodeSum)); 
					nodeSum = 0f;
				}
			}
			return nodes[nodeNumbers[nodeNumbers.Count - 1]].getNodeValue();
		}
	}
}


