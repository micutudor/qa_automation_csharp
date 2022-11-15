using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    class CardModel
    {
        public string Number { get; set; }
        
        public string ExpiringMonth { get; set; }

        public string ExpiringYear { get; set; } 

        public CardModel(string number, string expiringMonth, string expiringYear)
        {
            Number = number;
            ExpiringMonth = expiringMonth;
            ExpiringYear = expiringYear;
        }
    }
}
