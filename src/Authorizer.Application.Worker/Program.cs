using Authorizer.Application.Worker.Common;
using Authorizer.Application.Worker.Common.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Authorizer.Application.Worker
{
    public class Program
    {
        //
        private static IDeserializerJson<account> _deserializer;

        public static account Account;
        public static List<Input> Inputs;

        public static void ConfigureInstances()
        {
            _deserializer = new DeserializerJson<account>();

            Account = new account();
            Inputs = new List<Input>();
        }

        static void Main(string[] args)
        {
            try
            {
                ConfigureInstances();

                var te = Console.ReadLine();

                //StreamReader st = new StreamReader(te);

                var ret = JsonConvert.DeserializeObject<account>(te);

                Console.WriteLine(ret);

                Console.WriteLine(JsonConvert.DeserializeObject<account>(te));

                Console.WriteLine("Entrada : " + te);

                //var teste = _deserializer.Deserializer(st);
                
                //Console.WriteLine($"Active : { te.ActiveCard } Limit : { te.AvailableLimit } ");

                Console.ReadKey();

                //ReadInput();
                //AuthorizeOperations();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void ReadInput()
        {
            Inputs = new List<Input>();
            Console.WriteLine("Cat operations");
            do
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line)) { break; }
                Inputs.Add(JsonConvert.DeserializeObject<Input>(line));
            } while (true);
        }
                
        public static Output AccountCreation(account account)
        {
            var output = new Output() { Violations = new string[0] };

            if (account.activeCard)
            {
                output.Violations = new string[] { "account-already-initialized" };
            }
            else
            {
                account.activeCard = account.activeCard;
                account.availableLimit = account.availableLimit;
            }

            return output;
        }

        public static bool HighFrequency(Transaction current)
        {
            return TransactionsOnTwoMinutes(current) > 3;
        }

        public static int TransactionsOnTwoMinutes(Transaction current)
        {
            DateTime minutes = current.Time.AddMinutes(-2);
            int transactions = Inputs.Where(c => c.Transaction.Time >= minutes && c.Transaction.Time <= current.Time).Count();
            return transactions;
        }

        public static bool DoubledTransaction(Transaction current)
        {
            int transactions = TransactionsOnTwoMinutes(current);

            if (transactions > 2)
            {
                return Inputs.Where(c => c.Transaction.Merchant == current.Merchant && c.Transaction.Amount == current.Amount).Count() > 2;
            }
            return false;
        }


    }

    #region Entities

    public struct Input
    {
        public account Account { get; set; }
        public Transaction Transaction { get; set; }
    }

    public struct Output
    {
        public account Account { get; set; }
        public string[] Violations { get; set; }
    }

    public struct account
    {
        public bool activeCard { get; set; }
        public string availableLimit { get; set; }
    }

    public struct Transaction
    {
        public string Merchant { get; set; }
        public int Amount { get; set; }
        public DateTime Time { get; set; }
    }

    #endregion

}
