using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Linq;
using System;
using Random = System.Random;

namespace neat
{
    public class ControlRobots : MonoBehaviour {

        public GameObject neuralNetwork;
        public Text generationInformation;
        public Text genomeInformation; 
        public Transform spawnPoint;

        private GeneratePopulation population;
        private int [] indexes = new int[50];
        private List<Genome> networks; 
        private NEATPlayerOne [] scripts;
        private Random random;

        private Genome generateInitialGenome() {
            Genome initialGenome = new Genome();

            int node0 = initialGenome.getNodes().Count;
			initialGenome.addNodeGene(new NodeGene(NodeGene.NodeType.INPUT, node0));
			int node1 = initialGenome.getNodes().Count;
			initialGenome.addNodeGene(new NodeGene(NodeGene.NodeType.INPUT, node1));
			int node2 = initialGenome.getNodes().Count;
			initialGenome.addNodeGene(new NodeGene(NodeGene.NodeType.OUTPUT, node2));

			InnovationUtility.incrementGlobalInnovation();
			initialGenome.addConnectionGene(new ConnectionGene(node0, node2, Genome.getRandomFloatBetweenPoints(random, Utility.RANDOM_VAL_MIN, Utility.RANDOM_VAL_MAX), true, InnovationUtility.getGlobalInnovation()));

			InnovationUtility.incrementGlobalInnovation();
			initialGenome.addConnectionGene(new ConnectionGene(node1, node2, Genome.getRandomFloatBetweenPoints(random, Utility.RANDOM_VAL_MIN, Utility.RANDOM_VAL_MAX), true, InnovationUtility.getGlobalInnovation()));

            return initialGenome;
        }

        void setAllText(List<Genome> genomes) {
            generationInformation.text = "Generation: " + population.getGenerationCounter() + " Species: " + population.getSpeciesList().Count + " BestGenome: " + population.getBestPerformer().getConnections().Count;
            genomeInformation.text = "";
            genomeInformation.text += "Highest Performers Fitness: " + genomes[0].getFitness() + "\n";
        }

        void Start() {
            scripts = GetComponentsInChildren<NEATPlayerOne>();
            population = new GeneratePopulation(50);
            random = new Random();

            Genome initialGenome = generateInitialGenome();

            for(int i = 0; i < population.getPopulationSize(); i++) {
                population.addGenomeToPopulation(new Genome(initialGenome));
            }

            networks = population.GenerateNextGeneration(random);
            population.ReplaceWithNextGeneration();
            setAllText(networks);
        }

        void Update() {
            for(int i = 0; i < population.getPopulationSize(); i++) {
                evaluateNetwork(Utility.xPositionOfObstacles, Utility.heightOfObstacles, scripts[i], networks[i], ref indexes[i]);
            }
             
            if(scripts.All(x => x.gameObject.GetComponent<SpriteRenderer>().isVisible == false)) {
                population.setPopulationList(networks);
                networks = population.GenerateNextGeneration(random);
                networks.ForEach(network => network.setJumpsMade(0));
                population.ReplaceWithNextGeneration();
                setAllText(networks);
                Respawn();
            }
        }
    
        public void Respawn () {
            foreach(NEATPlayerOne script in scripts) {
                script.gameObject.GetComponent<Renderer>().enabled = true;
                script.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            }
            
            foreach(NEATPlayerOne script in scripts) {
                script.gameObject.transform.position = spawnPoint.position;
            }

            Array.Clear(indexes, 0, indexes.Length);
        }

        void evaluateNetwork(List <float> xPosOfObstacles, List <float> heightsOfObstacles, NEATPlayerOne script, Genome robotController, ref int index) {
            if(script.transform.position.x > xPosOfObstacles[index]) {
                index++;
            }

            // setting death position in genome
            robotController.setDistanceTravelled(script.getDeathPositionX());

            float output = robotController.calculateOutput(xPosOfObstacles[index] - script.transform.position.x, heightsOfObstacles[index]);
  
            if(output > 0.0 && script.getOnGround() && population.getGenerationCounter() > 1) {
                script.PlayerJump();
                robotController.addToJumpsMade();
            } 
        }
    }
}

