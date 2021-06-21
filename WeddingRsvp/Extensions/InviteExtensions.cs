using WeddingRsvp.Data;
using WeddingRsvp.Protos;

namespace WeddingRsvp.Extensions
{
    public static class InviteExtensions
    {
        public static InviteAdminViewModel ToInviteAdminViewModel(this Invite invite)
        {
            return new InviteAdminViewModel()
            {
                CreatedAt = invite.CreatedAt.ToDateTimeOffset(),
                Email = invite.Email,
                Id = invite.Id,
                InviteCode = invite.InviteCode,
                NumberOfPeople = invite.NumberOfPeople,
                PartyName = invite.PartyName,
                People = invite.People,
                RsvpStatus = invite.RsvpStatus,
                UpdatedAt = invite.UpdatedAt.ToDateTimeOffset(),
            };
        }

        public static UpdateInviteRequest ToUpdateInviteReqest(this InviteAdminViewModel inviteViewModel)
        {
            return new UpdateInviteRequest()
            {
                Email = inviteViewModel.Email,
                InviteId = inviteViewModel.Id,
                InviteCode = inviteViewModel.InviteCode,
                NumberOfPeople = inviteViewModel.NumberOfPeople,
                PartyName = inviteViewModel.PartyName,
                People = { inviteViewModel.People },
                RsvpStatus = inviteViewModel.RsvpStatus                
            };
        }
    }
}
