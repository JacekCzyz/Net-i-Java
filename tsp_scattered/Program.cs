using evolution;
using Graph;
using MPI;
using System.Text;
using System.Text.Json;


class Program
{
    static void Main(string[] args)
    {
        using (new MPI.Environment(ref args))
        {
            Intracommunicator comm = Communicator.world;
            int rank = comm.Rank;
            int size = comm.Size;

            Console.WriteLine($"Rank {rank} is starting.");

            MapGraph mapGraph = null;

            if (rank == 0)
            {
                mapGraph = new MapGraph();
                mapGraph.generate_map(500, 4);
            }

            if (rank == 0)
            {
                byte[] mapGraphBytes = SerializeToByteArray(mapGraph);
                int dataSize = mapGraphBytes.Length;
                comm.ImmediateSend(dataSize, 1, 0);
                comm.ImmediateSend(mapGraphBytes, 1, 1);
            }
            else if (rank == 1)
            {
                int dataSize = 0;
                ReceiveRequest sizeRequest = comm.ImmediateReceive<int>(0, 0, (receivedData) =>
                {
                    dataSize = receivedData;
                });
                sizeRequest.Wait();
                byte[] mapGraphBytes = new byte[dataSize];
                ReceiveRequest mapRequest = comm.ImmediateReceive(0, 1, mapGraphBytes);
                mapRequest.Wait();
                mapGraph = DeserializeFromByteArray<MapGraph>(mapGraphBytes);
            }



            int populationSize = 50;
            int generations = 1000;
            int migrationFrequency = 100;
            comm.Barrier();

            algorithm_result localBest = algorithm.evolution_with_migration(mapGraph, generations, populationSize, comm, migrationFrequency);

            //string serializedLocalBest = localBest.ToJson();

            //byte[] serializedLocalBestBytes = Encoding.UTF8.GetBytes(serializedLocalBest);
            byte[] serializedLocalBestBytes = SerializeToByteArray(localBest);

            if (rank == 0)
            {
                comm.Barrier();

                int dataSize = 0;
                ReceiveRequest receiveSizeRequest = comm.ImmediateReceive<int>(1, 0, (receivedData) =>
                {
                    dataSize = receivedData;
                });
                receiveSizeRequest.Wait();

                byte[] receivedData = new byte[dataSize];

                comm.Barrier();

                ReceiveRequest receiveDataRequest = comm.ImmediateReceive(1, 1, receivedData);
                receiveDataRequest.Wait();

                algorithm_result deserializedResult = DeserializeFromByteArray<algorithm_result>(receivedData);
                Console.WriteLine($"Best got in rank 1: {deserializedResult.dPathLen}");
                Console.WriteLine($"Best got in rank 0: {localBest.dPathLen}");

                if (deserializedResult.dPathLen < localBest.dPathLen)
                {
                    localBest = deserializedResult;
                }
                
                Console.WriteLine($"Best path: {localBest.dPathLen}");
            }
            else if (rank == 1)
            {
                comm.Barrier();
                byte[] byteArrayToSend = SerializeToByteArray(localBest);
                int dataSize = byteArrayToSend.Length;
                comm.ImmediateSend(dataSize, 0, 0);
                comm.Barrier();
                comm.ImmediateSend(byteArrayToSend, 0, 1);
            }
        }
    }
    public static byte[] SerializeToByteArray(object obj)
    {
        string jsonString = JsonSerializer.Serialize(obj);
        return Encoding.UTF8.GetBytes(jsonString);
    }

    public static T DeserializeFromByteArray<T>(byte[] byteArray)
    {
        string jsonString = Encoding.UTF8.GetString(byteArray);
        return JsonSerializer.Deserialize<T>(jsonString);
    }
}