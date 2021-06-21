using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;

namespace WeddingRsvp.Api.Data
{
    public class Invite : Document
    {
        public string PartyName { get; set; }

        public int NumberOfPeople { get; set; }

        public string InviteCode { get; set; }

        public IEnumerable<Person> People { get; set; }

        public RsvpStatus RsvpStatus { get; set; }

        public DateTimeOffset CreatedAt {get; set;}

        public DateTimeOffset UpdatedAt {get; set;}

        public string Email { get; internal set; }

        public Invite()
        {

        }
    }
}
