using Grpc.Core;
using Grpc.Net.Client;
using ModelLibrary.GRPC;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ModelLibrary.GRPC.MeteoriteLandingsService;

namespace RESTvsGRPC
{
    public class GRPCClient
    {
        private readonly GrpcChannel channel;
        private readonly MeteoriteLandingsServiceClient client;

        public GRPCClient()
        {
            channel = GrpcChannel.ForAddress("localhost:5006");
            client = new MeteoriteLandingsServiceClient(channel);
        }

        public async Task<string> GetSmallPayloadAsync()
        {
            return (await client.GetVersionAsync(new EmptyRequest())).ApiVersion;
        }

        public async Task<List<MeteoriteLanding>> StreamLargePayloadAsync()
        {
            List<MeteoriteLanding> meteoriteLandings = new List<MeteoriteLanding>();

            using (var response = client.GetLargePayload(new EmptyRequest()))
            {
                var responseStream = response.ResponseStream;
                while (await responseStream.MoveNext())
                {
                    meteoriteLandings.Add(responseStream.Current);
                }
            }

            return meteoriteLandings;
        }

        public async Task<IList<MeteoriteLanding>> GetLargePayloadAsListAsync()
        {
            return (await client.GetLargePayloadAsListAsync(new EmptyRequest())).MeteoriteLandings;
        }

        public async Task<string> PostLargePayloadAsync(MeteoriteLandingList meteoriteLandings)
        {
            return (await client.PostLargePayloadAsync(meteoriteLandings)).Status;
        }
    }
}
