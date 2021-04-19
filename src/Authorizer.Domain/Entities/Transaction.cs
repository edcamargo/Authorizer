using Newtonsoft.Json;
using System;

namespace Authorizer.Domain.Entities
{
    public class Transaction : Entity
    {
        public Transaction()
        { }

        public Transaction(string merchant, int amount, DateTime time)
        {
            Merchant = merchant;
            Amount = amount;
            Time = time;
        }

        [JsonProperty(PropertyName = "merchant")]
        public string Merchant { get; private set; }

        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; private set; }

        [JsonProperty(PropertyName = "time")]
        public DateTime Time { get; private set; }
    }

    public class RootTransaction
    {
        public Transaction transaction { get; set; }
    }
}
