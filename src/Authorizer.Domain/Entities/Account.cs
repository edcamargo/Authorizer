using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authorizer.Domain.Entities
{
    public class Account
    {
        public Account() { }

        public Account(bool activecard, int availablelimit)
        {
            ActiveCard = activecard;
            AvailableLimit = availablelimit;
        }

        [Key]
        [JsonProperty(PropertyName = "active-card")]
        public bool ActiveCard { get; private set; }

        [JsonProperty(PropertyName = "available-limit")]
        public int AvailableLimit { get; private set; }

        [NotMapped]
        [JsonProperty(PropertyName = "violations")]
        public List<string> Violations { get; private set; } = new();

        public void ThrowsViolation(string Message)
        {
            if (!Violations.Contains(Message))
            {
                //Violations.AddRange(new List<string> { Message });
                Violations.Add(Message);
            }
        }

        public int Transaction(int amountTransation)
        {
            var newAvailableLimit = AvailableLimit - amountTransation;
            return newAvailableLimit;
        }
    }

    public class RootAccount
    {
        public Account account { get; set; }
    }
}
