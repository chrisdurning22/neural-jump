using System;
using System.Configuration;

namespace neat
{
	public class ConnectionGene {
		private int input;
		private int output;
		private float weight;
		private bool enabled;
		private int innovationNumber;

		public ConnectionGene(ConnectionGene gene) {
			this.input = gene.input;
			this.output = gene.output;
			this.weight = gene.weight;
			this.enabled = gene.enabled;
			this.innovationNumber = gene.innovationNumber;
		}

		public ConnectionGene(int input, int output, float weight, bool enabled, int innovationNumber) {
			this.input = input;
			this.output = output;
			this.weight = weight;
			this.enabled = enabled;
			this.innovationNumber = innovationNumber;
		}

		public int getIn() {
			return input;
		}

		public int getOut() {
			return output;
		}

		public float getWeight() {
			return weight;
		}

		public bool getEnabled() {
			return enabled;
		}

		public int getInnovationNumber() {
			return innovationNumber;
		}

		public void setEnabled (bool enabled) {
			this.enabled = enabled;
		}

		public void setWeight(float weight) {
			this.weight = weight;
		}
			
	}
}

