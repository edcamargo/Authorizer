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

        public string Merchant { get; private set; }
        public int Amount { get; private set; }
        public DateTime Time { get; private set; }
    }

    public class RootTransaction
    {
        public Transaction Transaction { get; set; }
    }
}
