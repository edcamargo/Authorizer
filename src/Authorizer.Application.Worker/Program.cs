using Authorizer.Application.Worker.Common;
using Authorizer.Application.Worker.Common.Interfaces;
using Authorizer.Application.Worker.Servives;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Authorizer.Application.Worker
{
    public class Program
    {
        public static account Account;
        public static List<Input> Inputs;

        //public static void ConfigureInstances()
        //{
        //    _deserializer = new DeserializerJson<account>();

        //    Account = new account();
        //    Inputs = new List<Input>();
        //}

        public static void Main(string[] args)
        {
            try
            {
                var service = new ServiceCollection();
                var services = Startup.ConfigureServices(service);
                var serviceProvider = services.BuildServiceProvider();

                serviceProvider.GetService<EntryPoint>().Run(args);

                //StreamReader file = new StreamReader(@"C:\Projetos\Authorizer\json.txt");
                //_deserializer.Deserializer(file);

                //var te = Console.ReadLine();

                //var ret = JsonConvert.DeserializeObject<account>(te);

                //Console.WriteLine(ret);

                //Console.WriteLine(JsonConvert.DeserializeObject<account>(te));

                //Console.WriteLine("Entrada : " + te);


                Console.ReadKey();
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
