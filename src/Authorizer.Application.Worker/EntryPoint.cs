using Authorizer.Domain.Enums;
using Authorizer.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Authorizer.Application.Worker
{
    public class EntryPoint
    {
        private readonly IAccountService _openAccountService;
        private readonly ILogger<EntryPoint> _logger;

        public EntryPoint(IAccountService openAccountService, ILogger<EntryPoint> logger)
        {
            _openAccountService = openAccountService;
            _logger = logger;
        }

        public void Run(String[] args)
        {
            _logger.LogInformation("Init Test!");
            using (StreamReader reader = new StreamReader(Console.OpenStandardInput()))
            {
                var stdin = "";
                do
                {
                    var _command = JsonConvert.DeserializeObject<object>(Console.ReadLine()).ToString();

                    switch (_command)
                    {
                        case string a when a.Contains(Operations.ACCOUNT.ToString().ToLower()):
                            var returnMessage = _openAccountService.Execute(_command);
                            Console.WriteLine(returnMessage);
                            break;
                        case string a when a.Contains(Operations.TRANSACTION.ToString().ToLower()):
                            Console.WriteLine("Transation not implemented");
                            break;
                        case null:
                            new ArgumentNullException(nameof(_command));
                            break;
                    }

                } while (stdin != null);
            }

            Console.ReadKey();
        }
    }
}
