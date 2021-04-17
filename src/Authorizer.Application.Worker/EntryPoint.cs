using Authorizer.Application.Worker.Servives;
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
        private readonly IAccount _account;
        private readonly ILogger<EntryPoint> _logger;

        public EntryPoint(IAccount account, ILogger<EntryPoint> logger)
        {
            _account = account;
            _logger = logger;
        }

        public void Run(String[] args)
        {
            _logger.LogDebug("The coefficients have been set!");

            /*
            var nome = _account.RetornarValor("");

            var test = JsonConvert.DeserializeObject<object>(Console.ReadLine());

            Console.WriteLine($"Command 1 : { test }");

            Console.WriteLine("Qual é o seu nome?");
            var name = Console.ReadLine();

            Console.WriteLine("Quantos anos você tem?");
            var idade = Console.ReadLine();

            Console.WriteLine($"Command 2 : { name } - { idade }");


            Console.WriteLine($"Command : { Console.ReadLine() }");

            Console.WriteLine($"Nome : { nome }");
            */

            /*
            SpringApplication.run(AuthorizerApplication.class, args);

		    Scanner scanner = new Scanner(System.in);
            ObjectMapper objectMapper = new ObjectMapper();
            objectMapper.findAndRegisterModules();

		    while (scanner.hasNext()) {
			    command = scanner.nextLine();
			    AccountDTO accountDTO = getAccountOperation(command, objectMapper);
                TransactionDTO transactionDTO = getTransactionOperation(command, objectMapper);

                accountFlow(objectMapper, accountDTO);
                transactionFlow(objectMapper, transactionDTO);
            }
            */


            List<string> salesLines = new List<string>();
            Console.InputEncoding = Encoding.UTF8;
            using (StreamReader reader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding))
            {
                string stdin = "";

                do
                {
                    StringBuilder stdinBuilder = new StringBuilder();

                    var test = JsonConvert.DeserializeObject<object>(Console.ReadLine());

                    //Console.WriteLine($"Command 1 : { test }");

                    //stdin = reader.ReadLine();

                    stdinBuilder.Append(test);
                    var lineIn = test;
                    if (stdinBuilder.ToString().Trim() != "")
                    {
                        salesLines.Add(stdinBuilder.ToString().Trim());
                    }

                    foreach (var item in salesLines)
                    {
                        Console.WriteLine(item);
                    }

                } while (stdin != null);
            }

            Console.ReadKey();
        }
    }
}
