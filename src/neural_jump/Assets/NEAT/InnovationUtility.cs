using System;

namespace neat
{
	public class InnovationUtility
	{
		public static int globalInnovation = 0;

		public static void incrementGlobalInnovation() {
			globalInnovation++;
		}

		public static int getGlobalInnovation() {
			return globalInnovation;
		}

		public static void setGlobalInnovation(int innovation) {
			globalInnovation = innovation;
		}

	}
}

