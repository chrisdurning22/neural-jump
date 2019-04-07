using System;
using System.Collections.Generic;
using System.Configuration;

namespace neat
{
	public class Species {
        private float speciesFitness;
        private Genome representative;
        private List<Genome> genomes;

        // first genome of the species
        public Species(Genome genome) {
            speciesFitness = 0f;
            this.representative = genome;
            genomes = new List<Genome>();
            genomes.Add(genome);
        }

        public void addGenome(Genome genome) {
            genomes.Add(genome);
        }

        public List<Genome> getGenomesList() {
            return genomes;
        }

        public void setGenomeList(List<Genome> genomes) {
            this.genomes = genomes;
        }

        public void addGenomeToSpecies(Genome genome) {
            genomes.Add(genome);
        }

        public void clearSpeciesGenomesList() {
            this.genomes.Clear();
        }

        public void setRepresentative(Genome representative) {
            this.representative = representative;
        }

        public Genome getRepresentative() {
            return this.representative;
        }

        public void setSpeciesFitness(float speciesFitness) {
            this.speciesFitness = speciesFitness;
        }

        public float getSpeciesFitness() {
            return this.speciesFitness;
        }

        public void addFitnessToTotal(float fitness) {
            this.speciesFitness += fitness;
        }
	}
}