using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeddingRsvp.Extensions;
using WeddingRsvp.Protos;

namespace WeddingRsvp.Data
{
    public class InviteManagementService
    {
        public async Task<InviteAdminViewModel[]> GetInvites()
        {
            var handler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
            {
                HttpClient = new HttpClient(handler)
            });

            var invites = new BlockingCollection<InviteAdminViewModel>();

            var client = new Inviter.InviterClient(channel);
            using var stream = client.GetAllInvites(new GetAllInvitesRequest());

            await foreach (var response in stream.ResponseStream.ReadAllAsync())
            {
                invites.Add(response.ToInviteAdminViewModel());
            }

            return invites.ToArray();
        }
    }
}
