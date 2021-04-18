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
        public List<string> Violations { get; private set; }

        public void ThrowsViolation(List<string> Message)
        {
            Violations = Message;
        }

        public int Transaction(int amountTransation)
        {
            var newAvailableLimit = AvailableLimit - amountTransation;
            return newAvailableLimit;
        }
    }    
}
