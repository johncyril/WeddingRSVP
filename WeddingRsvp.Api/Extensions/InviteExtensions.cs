using Google.Protobuf.WellKnownTypes;
using System;
using System.Linq;

namespace WeddingRsvp.Api.Extensions
{
    public static class InviteExtensions
    {
        public static Data.Invite ToInviteDbObect(this Protos.Invite inviteDto)
        {
            return new Data.Invite()
            {
                Id = string.IsNullOrEmpty(inviteDto.Id) ? default : Guid.Parse(inviteDto.Id),
                PartyName = inviteDto.PartyName,
                InviteCode = inviteDto.InviteCode,
                NumberOfPeople = inviteDto.NumberOfPeople,
                Email = inviteDto.Email,
                People = inviteDto.People.Select(x => x.ToPersonDbObject()),
                RsvpStatus = (Data.RsvpStatus)inviteDto.RsvpStatus,
                CreatedAt = inviteDto.CreatedAt.ToDateTimeOffset(),
                UpdatedAt = inviteDto.UpdatedAt.ToDateTimeOffset()
            };
        }

        public static Protos.Invite ToInviteDto(this Data.Invite inviteDbObject)
        {
            return new Protos.Invite()
            {
                Id = inviteDbObject.Id.ToString(),
                PartyName = inviteDbObject.PartyName,
                InviteCode = inviteDbObject.InviteCode,
                NumberOfPeople = inviteDbObject.NumberOfPeople,
                Email = inviteDbObject.Email,
                People = { inviteDbObject.People.Select(x => x.ToPersonDto()) },
                RsvpStatus = (Protos.RsvpStatus)inviteDbObject.RsvpStatus,
                CreatedAt = Timestamp.FromDateTimeOffset(inviteDbObject.CreatedAt),
                UpdatedAt = Timestamp.FromDateTimeOffset(inviteDbObject.UpdatedAt)
            };
        }
    }
}
