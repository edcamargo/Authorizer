using System;

namespace Authorizer.Domain.Entities
{
    public class Transaction
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public string merchant { get; set; }
        public int amount { get; set; }
        public DateTime time { get; set; }
    }

    public class RootTransaction
    {
        public Transaction transaction { get; set; }
    }
}
