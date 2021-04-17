using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Authorizer.Domain.Entities
{
    public class Account
    {
        public Account(bool activecard, Int32 availablelimit)
        {
            ActiveCard = activecard;
            AvailableLimit = availablelimit;
        }

        [JsonProperty("active-card")]
        public bool ActiveCard { get; private set; }

        [JsonProperty("available-limit")]
        public int AvailableLimit { get; private set; }

        public int Transaction(int AvailableTransation)
        {
            var Acumulativo = AvailableLimit - AvailableTransation;
            return Acumulativo;
        }
    }
    
    public class RootAccount
    {
        public Account account { get; set; }
        public List<string> violations { get; set; }
    }
}
