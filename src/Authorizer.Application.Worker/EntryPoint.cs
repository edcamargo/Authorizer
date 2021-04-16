using Authorizer.Application.Worker.Servives;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

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

            var nome = _account.RetornarValor("");

            var test = JsonConvert.DeserializeObject<object>(Console.ReadLine());

            Console.WriteLine($"Command 1 : { test }");

            Console.WriteLine($"Command : { Console.ReadLine() }");

            Console.WriteLine($"Nome : { nome }");
        }
    }
}
