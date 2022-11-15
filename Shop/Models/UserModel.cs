using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    class UserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public CardModel Card { get; set; }

        public UserModel(dynamic firstName, dynamic lastName, dynamic country, dynamic city, CardModel card)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            City = city;
            Card = card;
        }
    }
}
