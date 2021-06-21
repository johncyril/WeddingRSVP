using Grpc.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using WeddingRsvp.Api.Data;
using WeddingRsvp.Api.Exceptions;
using WeddingRsvp.Api.Extensions;
using WeddingRsvp.Protos;
using WeddingRsvp.Api.Utilities;
using Invite = WeddingRsvp.Protos.Invite;

namespace WeddingRsvp.Api.Services
{
    public class InviterService : Inviter.InviterBase
    {
        private readonly IMongoDbRepository _repo;

        public InviterService(IMongoDbRepository repository)
        {
            _repo = repository;
        }

        public override async Task<Invite> AddInvite(AddInviteRequest request, ServerCallContext context)
        {
            var inviteToAdd = new Data.Invite()
            {
                PartyName = request.PartyName,
                NumberOfPeople = request.NumberOfPeople,
                InviteCode = InviteCodeGenerator.GenerateCode(),
                Email = request.Email,
                People = request.People.Select(x => x.ToPersonDbObject()).ToArray(),
                RsvpStatus = Data.RsvpStatus.Unknown,
                CreatedAt = DateTime.UtcNow
            };
            var addedInvite = await _repo.AddOneAsync(inviteToAdd);
            return addedInvite.ToInviteDto();
        }    

        public override async Task GetAllInvites(GetAllInvitesRequest request, IServerStreamWriter<Invite> responseStream, ServerCallContext context)
        {
            var invites = await _repo.GetAllAsync<Data.Invite>(x => true);

            foreach (var invite in invites)
            {
                await responseStream.WriteAsync(invite.ToInviteDto());
            }
        }

        public override async Task<Invite> UpdateInvite(UpdateInviteRequest request, ServerCallContext context)
        {
            var invite = await _repo.GetOneAsync<Data.Invite>(Guid.Parse(request.InviteId));
            invite.Email = string.IsNullOrWhiteSpace(request.Email) ? invite.Email : request.Email;
            invite.PartyName = string.IsNullOrWhiteSpace(request.PartyName) ? invite.PartyName : request.PartyName;
            invite.NumberOfPeople = request.NumberOfPeople == default ? invite.NumberOfPeople : request.NumberOfPeople;
            invite.People = !request.People.Any() ? invite.People : request.People.Select(x => x.ToPersonDbObject()).ToArray();
            invite.RsvpStatus = request.RsvpStatus == default ? invite.RsvpStatus : (Data.RsvpStatus) ((int) request.RsvpStatus);
            invite.UpdatedAt = DateTime.UtcNow;
            invite.InviteCode = string.IsNullOrWhiteSpace(request.InviteCode) ? invite.InviteCode : request.InviteCode;

            var updatedInvite = await _repo.UpdateOneAsync(invite);

            return updatedInvite.ToInviteDto();
        }

        public override async Task<Invite> GetInviteByCode(GetInviteByCodeRequest request, ServerCallContext context)
        {
            var invite = await _repo.GetAllAsync<Data.Invite>(x => x.InviteCode == request.InviteCode && x.PartyName == request.PartyName);

            if (!invite.Any())
            {
                throw new InviteNotFoundException($"An invite with code:{request.InviteCode} and party name:{request.PartyName} was not found");
            }

            if (invite.Count() > 1)
            {
                throw new MultipleInvitesFoundException($"There appear to be {invite.Count()} invites with code:{request.InviteCode} and party name:{request.PartyName}");
            }

            return invite.First().ToInviteDto();
        }

    }
}
