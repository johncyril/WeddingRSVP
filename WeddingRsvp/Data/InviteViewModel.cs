using System;
using System.Collections.Generic;
using WeddingRsvp.Protos;

namespace WeddingRsvp.Data
{
    public class InviteViewModel
    {
        public string Id { get; set; }

        public string PartyName { get; set; }

        public int NumberOfPeople { get; set; }

        public string InviteCode { get; set; }

        public List<PersonViewModel> People { get; set; }

        public RsvpStatus RsvpStatus { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public string Email { get; internal set; }
    }
}
