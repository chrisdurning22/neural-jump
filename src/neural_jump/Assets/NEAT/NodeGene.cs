using System;
using System.Configuration;

namespace neat
{
	public class NodeGene {
		private int nodeNumber;
		private float value;

		public enum NodeType {
			INPUT, 
			HIDDEN,
			OUTPUT
		};

		private NodeType type;

		public NodeGene(NodeGene another) {

			this.type = another.type;
			this.nodeNumber = another.nodeNumber;
		}

		public NodeGene(NodeType type, int nodeNumber) {
			
			this.type = type;
			this.nodeNumber = nodeNumber;
		}

		public NodeType getNodeType() {
			return type;
		}

		public int getNodeNumber() {
			return nodeNumber;
		}

		public void setNodeValue(float value) {
			this.value = value;
		}

		public float getNodeValue() {
			return value;
		}
	}
}

