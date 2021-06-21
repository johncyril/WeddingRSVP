using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeddingRsvp.Data
{
    public class InviteManagementService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<Invite[]> GetInvites()
        {
            var handler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
            {
                HttpClient = new HttpClient(handler)
            });

            var client = new Inviter.InviterClient()
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new InviteAdminDto
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }
    }
}
