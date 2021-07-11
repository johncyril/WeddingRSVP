using System.Linq;
using WeddingRsvp.Data;
using WeddingRsvp.Protos;

namespace WeddingRsvp.Extensions
{
    public static class InviteExtensions
    {
        public static InviteViewModel ToInviteAdminViewModel(this Invite invite)
        {
            return new InviteViewModel()
            {
                CreatedAt = invite.CreatedAt.ToDateTimeOffset(),
                Email = invite.Email,
                Id = invite.Id,
                InviteCode = invite.InviteCode,
                NumberOfPeople = invite.NumberOfPeople,
                PartyName = invite.PartyName,
                People = invite.People.Select(x => x.ToPersonViewModel()).ToList(),
                RsvpStatus = invite.RsvpStatus,
                UpdatedAt = invite.UpdatedAt.ToDateTimeOffset(),
            };
        }

        public static UpdateInviteRequest ToUpdateInviteReqest(this InviteViewModel inviteViewModel)
        {
            return new UpdateInviteRequest()
            {
                Email = inviteViewModel.Email,
                InviteId = inviteViewModel.Id,
                InviteCode = inviteViewModel.InviteCode,
                NumberOfPeople = inviteViewModel.NumberOfPeople,
                PartyName = inviteViewModel.PartyName,
                People = { inviteViewModel.People.Select(x => x.ToPersonDto()) },
                RsvpStatus = inviteViewModel.RsvpStatus                
            };
        }

        public static AddInviteRequest ToAddInviteReqest(this InviteViewModel inviteViewModel)
        {
            return new AddInviteRequest()
            {
                Email = inviteViewModel.Email,
                NumberOfPeople = inviteViewModel.NumberOfPeople,
                PartyName = inviteViewModel.PartyName,
                People = { inviteViewModel.People.Select(x => x.ToPersonDto()) }               
            };
        }

        public static PersonViewModel ToPersonViewModel(this Protos.Person personDto)
        {
            return new PersonViewModel
            {
                RsvpStatus = personDto.RsvpStatus,
                DietaryRequirements = personDto.DietaryRequirements,
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                Id = personDto.Id
            };
        }

        public static Protos.Person ToPersonDto(this PersonViewModel personVm)
        {
            return new Person
            {
                Id = personVm.Id,
                LastName = personVm.LastName,
                FirstName = personVm.FirstName,
                DietaryRequirements = personVm.DietaryRequirements,
                RsvpStatus = personVm.RsvpStatus
            };
        }
    }
}
