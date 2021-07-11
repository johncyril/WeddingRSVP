﻿using WeddingRsvp.Protos;

namespace WeddingRsvp.Data
{
    public class PersonViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DietaryRequirements DietaryRequirements { get; set; }

        public RsvpStatus RsvpStatus { get; set; }

    }
}
