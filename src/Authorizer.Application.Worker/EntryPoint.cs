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
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly ILogger<EntryPoint> _logger;

        public EntryPoint(IAccountService accountService, 
                          ITransactionService transactionService, 
                          ILogger<EntryPoint> logger)
        {
            _accountService = accountService;
            _transactionService = transactionService;
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
                    object returnMessage;

                    switch (_command)
                    {
                        case string a when a.Contains(Operations.ACCOUNT.ToString().ToLower()):
                            returnMessage = _accountService.Execute(_command);
                            Console.WriteLine(returnMessage);

                            break;
                        case string a when a.Contains(Operations.TRANSACTION.ToString().ToLower()):
                            returnMessage = _transactionService.Execute(_command);
                            Console.WriteLine(returnMessage);

                            break;
                        case null:
                            Console.WriteLine("Transaction not implemented!");
                            break;
                    }

                } while (stdin != null);
            }

            Console.ReadKey();
        }
    }
}
