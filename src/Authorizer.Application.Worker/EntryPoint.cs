using Authorizer.Application.Worker.Dtos;
using Authorizer.Domain.Entities;
using Authorizer.Domain.Repositories;
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
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<EntryPoint> _logger;

        public EntryPoint(IAccountRepository accountRepository, ILogger<EntryPoint> logger)
        {
            _accountRepository = accountRepository;
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
                    // convert para objDto
                    var objAccountDto = JsonConvert.DeserializeObject<RootAccountDto>(Console.ReadLine());

                    // Cria a conta
                    var account = new Account(objAccountDto.account.ActiveCard, objAccountDto.account.AvailableLimit);
                    _accountRepository.Create(account);

                    // Exibe os dados de entrada
                    var retorno = JsonConvert.SerializeObject(objAccountDto);

                    StringBuilder stdinBuilder = new StringBuilder(retorno);

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
