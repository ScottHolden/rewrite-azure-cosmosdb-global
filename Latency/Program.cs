using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace GlobalDemo
{
    class Program
    {
        private static readonly string Endpoint = ConfigurationManager.AppSettings["Endpoint"];
        private static readonly string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];

        private static readonly string DatabaseName = ConfigurationManager.AppSettings["DatabaseName"];
        private static readonly string ContainerName = ConfigurationManager.AppSettings["ContainerName"];

        private static CosmosClient client;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var clientOptions = new CosmosClientOptions
            {
                ConnectionMode = ConnectionMode.Direct, // TCP
                ApplicationPreferredRegions = new[] {
                    Regions.WestUS2,
                    Regions.FranceCentral
				}
            };

            client = new CosmosClient(Endpoint, PrimaryKey, clientOptions);

            var container = client.GetContainer(DatabaseName, ContainerName);

            QueryDefinition query = new QueryDefinition("SELECT TOP 1 * FROM c WHERE c.playerId = @playerId")
                .WithParameter("@playerId", "test");

            while (true)
            {
                var sw = new Stopwatch();
                sw.Start();

                List<dynamic> results = new List<dynamic>();
                using (FeedIterator<dynamic> resultSetIterator = container.GetItemQueryIterator<dynamic>(query))
                {
                    while (resultSetIterator.HasMoreResults)
                    {
                        FeedResponse<dynamic> response = await resultSetIterator.ReadNextAsync();
                        results.AddRange(response);
                    }
                }

                var document = results.FirstOrDefault();

                //Console.WriteLine(document);

                sw.Stop();

                Console.WriteLine($"Query document in {sw.ElapsedMilliseconds} ms");

                Thread.Sleep(100);
            }
        }
    }
}
