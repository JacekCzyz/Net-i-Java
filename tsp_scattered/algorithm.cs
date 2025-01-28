using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Graph;
using MPI;

namespace evolution
{
    //[Serializable]
    public class algorithm_result
    {
        public double dPathLen { get; set; }
        public int[] permutation { get; set; }
    }
    public class algorithm
    {
        public static algorithm_result example(MapGraph Graph)
        {
            algorithm_result result = new algorithm_result();
            result.permutation = new int[Graph.intAmountVertexes];
            for (int i = 0; i < Graph.intAmountVertexes; i++)
            {
                result.permutation[i] = i;
            }
            result.dPathLen = calculate_len(result.permutation, Graph);

            return result;
        }

        //public static algorithm_result evolution(MapGraph Graph, int generations, int population_size)
        //{
        //    List<int[]> selected_specimen = new List<int[]>();
        //    int specimen_amount;
        //    double mutation_factor = 1.0;

        //    int intAmountVertexes = Graph.intAmountVertexes;
        //    algorithm_result localResult = new algorithm_result();

        //    List<int[]> population = generate_init_population(population_size, intAmountVertexes);

        //    for (int i = 0; i < generations; i++)
        //    {
        //        selected_specimen = selection(population, population_size, (int)Math.Sqrt(population_size), Graph);
        //        specimen_amount = selected_specimen.Count();
        //        population = generate_population(selected_specimen, (int)Math.Pow(specimen_amount, 2), intAmountVertexes, mutation_factor, 4);
        //        mutation_factor *= 0.99;
        //    }

        //    selected_specimen = selection(population, population_size, (int)Math.Sqrt(population_size), Graph);

        //    localResult.dPathLen = calculate_len(selected_specimen[0], Graph);
        //    localResult.permutation = selected_specimen[0];

        //    return localResult;
        //}

        public static algorithm_result evolution_with_migration(
    MapGraph Graph,
    int generations,
    int population_size,
    Intracommunicator comm,
    int migrationFrequency)
        {
            int rank = comm.Rank;

            List<int[]> population = generate_init_population(population_size, Graph.intAmountVertexes);
            Console.WriteLine(rank);

            for (int generation = 0; generation < generations; generation++)
            {
                population = selection(population, population_size, (int)Math.Sqrt(population_size), Graph);

                population = generate_population(population, population_size, Graph.intAmountVertexes, 1.0);

                if (generation % migrationFrequency == 0)
                {
                    population = perform_migration(population, comm, Graph);
                }
            }

            algorithm_result localBest = population
                .Select(individual => new algorithm_result
                {
                    dPathLen = calculate_len(individual, Graph),
                    permutation = individual
                })
                .OrderBy(result => result.dPathLen)
                .First();


            return localBest;
        }

        public static List<int[]> perform_migration(List<int[]> localPopulation, Intracommunicator comm, MapGraph Graph)
        {
            int rank = comm.Rank;
            int size = comm.Size;

            List<int[]> migrants = selection(localPopulation, localPopulation.Count, localPopulation.Count/10, Graph);
            List<int[]> incomingMigrants = new List<int[]>();


            foreach (int[] array in migrants)
            {
                comm.Barrier(); 

                int partner = (rank + 1) % size;
                int source = (rank - 1 + size) % size;

                int[] incoming_migrant = new int[array.Length];

                if (rank == 0)
                {
                    Request sendRequest = comm.ImmediateSend(array, partner, 0);
                    sendRequest.Wait();
                    Request receiveRequest = comm.ImmediateReceive(source, 0, incoming_migrant);
                    receiveRequest.Wait();
                }
                else
                {
                    Request receiveRequest = comm.ImmediateReceive(source, 0, incoming_migrant);
                    receiveRequest.Wait();
                    Request sendRequest = comm.ImmediateSend(array, partner, 0);
                    sendRequest.Wait();
                }
                comm.Barrier();
                incomingMigrants.Add(incoming_migrant);
            }

            localPopulation.AddRange(incomingMigrants);
            localPopulation = selection(localPopulation, localPopulation.Count, localPopulation.Count / size, Graph);
            return localPopulation;
        }




        public static int[] Flatten(List<int[]> list)
        {
            List<int> flattened = new List<int>();
            foreach (var array in list)
            {
                flattened.Add(array.Length);
                flattened.AddRange(array);
            }
            return flattened.ToArray();
        }

        public static List<int[]> Reassemble(int[] flattened)
        {
            List<int[]> list = new List<int[]>();
            int i = 0;
            while (i < flattened.Length)
            {
                int length = flattened[i];
                i++;
                int[] array = new int[length];
                Array.Copy(flattened, i, array, 0, length);
                i += length;
                list.Add(array);
            }
            return list;
        }


        public static List<int[]> generate_init_population(int population_size, int vertex_amount)
        {
            List<int[]> new_population = new List<int[]>();
            Random random = new Random();
            for (int i = 0; i < population_size; i++)
            {
                new_population.Add(create_permutation(vertex_amount));
            }

            return new_population.OrderBy(x => random.Next()).ToList(); // Extra shuffle
        }

        public static List<int[]> generate_population(List<int[]> population, int population_size, int vertex_amount, double mutation_factor)
        {
            List<int[]> new_population = new List<int[]>();
            int[] temp_specimen = new int[vertex_amount];
            Random random = new Random();
            for (int i = 0; i < population.Count(); i++)
            {
                for (int j = 0; j < population.Count(); j++)
                {
                    if (i != j)
                    {
                        temp_specimen = cross_over(population[i], population[j]);
                        if (random.NextDouble() < mutation_factor)
                        {
                            mutate(temp_specimen);
                        }
                    }


                    else
                        temp_specimen = population[i];
                    new_population.Add(temp_specimen);
                }
            }
            return new_population;
        }

        public static int[] cross_over(int[] parent1, int[] parent2)
        {
            int length = parent1.Length;
            int[] child = new int[length];
            Array.Fill(child, -1); // Mark empty spots
            Random random = new Random();
            int start = random.Next(length / 3);
            int end = start + random.Next(length / 3, length - start);

            // Copy a segment from parent1
            HashSet<int> usedGenes = new HashSet<int>();
            for (int i = start; i < end; i++)
            {
                child[i] = parent1[i];
                usedGenes.Add(parent1[i]);
            }

            // Fill remaining positions from parent2 in order
            int index = 0;
            for (int i = 0; i < length; i++)
            {
                if (child[i] == -1) // Empty spot
                {
                    while (usedGenes.Contains(parent2[index])) { index++; }
                    child[i] = parent2[index++];
                }
            }

            return child;
        }
        public static void mutate(int[] specimen)
        {
            Random random = new Random();
            int start = random.Next(specimen.Length / 2);
            int end = start + random.Next(2, specimen.Length / 2);

            List<int> sublist = specimen.Skip(start).Take(end - start).ToList();
            sublist = sublist.OrderBy(x => random.Next()).ToList();

            for (int i = start; i < end; i++)
            {
                specimen[i] = sublist[i - start];
            }
        }
        public static List<int[]> selection(List<int[]> population, int population_size, int amount_to_select, MapGraph Graph)
        {
            List<int[]> selected_items = new List<int[]>();

            List<(double fitness, int[] specimen)> fitnessList = new List<(double, int[])>();
            double totalFitness = 0;

            for (int i = 0; i < population.Count; i++)
            {
                double pathLength = calculate_len(population[i], Graph);
                double fitness = 1 / (pathLength + 1);
                fitnessList.Add((fitness, population[i]));
                totalFitness += fitness;
            }

            Random random = new Random();
            for (int i = 0; i < amount_to_select; i++)
            {
                double randomValue = random.NextDouble() * totalFitness;
                double cumulativeFitness = 0;

                foreach (var item in fitnessList)
                {
                    cumulativeFitness += item.fitness;
                    if (cumulativeFitness >= randomValue)
                    {
                        selected_items.Add(item.specimen);
                        break;
                    }
                }
            }

            return selected_items;
        }

        public static double calculate_len(int[] permutation, MapGraph Graph)
        {
            double path = 0;

            for (int i = 0; i < permutation.Length - 1; i++)
            {
                path += Graph.calculate_path(
                    Graph.collVertexes[permutation[i]],
                    Graph.collVertexes[permutation[i + 1]]
                );
            }

            path += Graph.calculate_path(
                Graph.collVertexes[permutation[permutation.Length - 1]],
                Graph.collVertexes[permutation[0]]
            );

            return path;
        }


        public static int[] create_permutation(int amount)
        {
            List<int> numbers = new List<int>();
            for (int i = 0; i < amount; i++)
            {
                numbers.Add(i);
            }

            Random rng = new Random();
            for (int i = numbers.Count - 1; i > 0; i--)
            {
                int randomIndex = rng.Next(i + 1);
                int temp = numbers[i];
                numbers[i] = numbers[randomIndex];
                numbers[randomIndex] = temp;
            }
            return numbers.ToArray();
        }
    }
}
