using System;
using System.ComponentModel.DataAnnotations;

namespace Authorizer.Domain.Entities
{
    public class Transaction
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        [Key]
        public int TransactionId { get; private set; }
        public string merchant { get; private set; }
        public int amount { get; private set; }
        public DateTime time { get; private set; }
    }

    public class RootTransaction
    {
        public Transaction transaction { get; set; }
    }
}
