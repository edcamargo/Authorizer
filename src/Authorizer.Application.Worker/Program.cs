using Microsoft.Extensions.DependencyInjection;
using System;

namespace Authorizer.Application.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var service = new ServiceCollection();
                var services = Startup.ConfigureServices(service);
                var serviceProvider = services.BuildServiceProvider();

                serviceProvider.GetService<EntryPoint>().Run(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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
