using Authorizer.Domain.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Authorizer.Application.Worker
{
    public class EntryPoint
    {
        // service private readonly IAccount _account;
        private readonly ILogger<EntryPoint> _logger;

        public EntryPoint(ILogger<EntryPoint> logger)
        {
            _logger = logger;
        }

        public void Run(String[] args)
        {
            _logger.LogDebug("The coefficients have been set!");

            List<string> salesLines = new List<string>();
            Console.InputEncoding = Encoding.UTF8;
            using (StreamReader reader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding))
            {
                var stdin = "";
                do
                {
                    var objAccount = JsonConvert.DeserializeObject<RootTransaction>(Console.ReadLine());

                    Account account = new Account(true, 100);
                    var xxx = account.Transaction(100);


                    StringBuilder stdinBuilder = new StringBuilder(account.ToString());

                    if (stdinBuilder.ToString().Trim() != "")
                    {
                        salesLines.Add(stdinBuilder.ToString().Trim());
                    }

                    foreach (var item in salesLines)
                    {
                        Console.Clear();
                        Console.WriteLine(item);
                    }

                } while (stdin != null);
            }

            Console.ReadKey();
        }
    }
}
