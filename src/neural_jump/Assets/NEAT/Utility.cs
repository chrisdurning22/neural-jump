using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace neat
{
	public class Utility {
        // Speciation
        public static float COMPATIBILITY_THRESHOLD = 3f;
        public static int COEFFICIENT1 = 1;
		public static int COEFFICIENT2 = 1;
		public static int COEFFICIENT3 = 4;

        // GeneratePopulation
        public static int ADD_CONNECTION_CHANCE_PERCENTAGE = 5;
        public static int ADD_NODE_CHANCE_PERCENTAGE = 3;
        public static int MUTATION_CHANCE_PERCENTAGE = 80;
        public static int CROSSOVER_CHANCE_PERCENTAGE = 80;

        public static Dictionary<int, HashSet<int>> nodeConnections = new Dictionary<int, HashSet<int>>();

        public static float RANDOM_VAL_MAX = 4.0f;
        public static float RANDOM_VAL_MIN = -4.0f;

        // used in addMutation
        public static Dictionary<int, NodeGene> nodeGenes = new Dictionary<int, NodeGene>();

        public static List <float> xPositionOfObstacles = new List<float>(){1651.54f, 1655.55f, 1665.61f, 1675.61f, 1684.54f, 1698.62f, 1711.53f, 1721.61f, 1736.54f, 1755.53f, 1767.62f, 1780.52f, 1786.54f,1888f};
        public static List <float> heightOfObstacles = new List<float>(){0.0f, 0.0f, 0.4f, 0.4f, 0.0f, 0.4f, 0.0f, 0.4f, 0.0f, 0.65f, 0.4f, 0.0f, 0.0f, 0.0f};
    }
}