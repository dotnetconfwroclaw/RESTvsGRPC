using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ModelLibrary.Data;
using ModelLibrary.GRPC;

namespace GrpcService
{
    public class MeteoriteLandingService : MeteoriteLandingsService.MeteoriteLandingsServiceBase
    {
        private readonly ILogger<MeteoriteLandingService> _logger;
        public MeteoriteLandingService(ILogger<MeteoriteLandingService> logger)
        {
            _logger = logger;
        }

        public override Task<Version> GetVersion(EmptyRequest request, ServerCallContext context)
        {
            return Task.FromResult(new Version
            {
                ApiVersion = "API Version 1.0"
            });
        }

        public override async Task GetLargePayload(EmptyRequest request, IServerStreamWriter<MeteoriteLanding> responseStream, ServerCallContext context)
        {
            foreach (var meteoriteLanding in MeteoriteLandingData.GrpcMeteoriteLandings)
            {
                await responseStream.WriteAsync(meteoriteLanding);
            }
        }

        public override Task<MeteoriteLandingList> GetLargePayloadAsList(EmptyRequest request, ServerCallContext context)
        {
            return Task.FromResult(MeteoriteLandingData.GrpcMeteoriteLandingList);
        }

        public override Task<StatusResponse> PostLargePayload(MeteoriteLandingList request, ServerCallContext context)
        {
            return Task.FromResult(new StatusResponse { Status = "SUCCESS" });
        }
    }
}
