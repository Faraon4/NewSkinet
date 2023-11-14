using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    // this class will leave in the same table as the orders ---
    // it has nothing related to the address from the Core/Identity/Address
    public class Address
    {
        // we create a parameterless ctor because entityFramework need it for not complaining
        // complain when we do the migration
        public Address()
        {
        }

        public Address(string firstName, string lastName, string street,
                        string city, string state, string zipcode)
        {

            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            State = state;
            ZipCode = zipcode;
        }
        
        // We do not give Id because it will be own by the Order class entity
        // it will not be a separate table

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}