using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeddingRsvp.Api.Extensions
{
    public static class PersonExtensions
    {
        public static Data.Person ToPersonDbObject(this Protos.Person personDto)
        {
            return new Data.Person()
            {
                DietaryRequirements = (Data.DietaryRequirements)personDto.DietaryRequirements,
                FirstName = personDto.FirstName,
                LastName = personDto.LastName
            };
        }

        public static Protos.Person ToPersonDto(this Data.Person personDbObject)
        {
            return new Protos.Person()
            {
                DietaryRequirements = (Protos.DietaryRequirements)personDbObject.DietaryRequirements,
                FirstName = personDbObject.FirstName,
                LastName = personDbObject.LastName
            };
        }
    }
}
