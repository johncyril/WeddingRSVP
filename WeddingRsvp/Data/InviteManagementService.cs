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
        Inviter.InviterClient client { get; set; }

        public InviteManagementService()
        {
            var handler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
            {
                HttpClient = new HttpClient(handler)
            });
            client = new Inviter.InviterClient(channel);
        }

        public async Task<InviteViewModel[]> GetInvites()
        {
            var invites = new BlockingCollection<InviteViewModel>();
           
            using var stream = client.GetAllInvites(new GetAllInvitesRequest());

            await foreach (var response in stream.ResponseStream.ReadAllAsync())
            {
                invites.Add(response.ToInviteAdminViewModel());
            }

            return invites.ToArray();
        }

        public async Task<string> AddInvite(InviteViewModel model)
        {
            var response = await client.AddInviteAsync(model.ToAddInviteReqest());
            return response.InviteCode;
        }

        // Todo - this could be made more sophisticated to see if it has atcually been updated
        public async Task<string> UpdateInvite(InviteViewModel model)
        {
            var response = await client.UpdateInviteAsync(model.ToUpdateInviteReqest());
            return response.InviteCode;
        }
    }
}
