using Authorizer.Domain.Dtos.Account;
using Authorizer.Domain.Entities;
using Authorizer.Domain.Interfaces.Repositories;
using Authorizer.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Authorizer.InfraStructure.Data.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountService> _logger;
        private string MESSAGE;

        public AccountService(IAccountRepository accountRepository, ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public object Execute(string command)
        {
            var account = new Account();
            var accountDto = JsonConvert.DeserializeObject<RootAccountDto>(command);
            var ExistsActiveCard = _accountRepository.FindActiveCard(accountDto.account.ActiveCard);

            if (ExistsActiveCard == null)
                account = CreateAccount(accountDto);

            if (ExistsActiveCard != null)
                account = ReturnAccount(ExistsActiveCard);

            account.ThrowsViolation(new List<string>() { MESSAGE });

            return JsonConvert.SerializeObject(account);
        }

        public Account CreateAccount(RootAccountDto rootAccountDto)
        {
            _logger.LogInformation("Init Create Account!");

            var account = new Account(rootAccountDto.account.ActiveCard, rootAccountDto.account.AvailableLimit);
            return _accountRepository.Create(account);
        }

        public Account ReturnAccount(Account account)
        {
            _logger.LogInformation("Account Already Initialized!");

            MESSAGE = "account-already-initialized";           
            return new Account(account.ActiveCard, account.AvailableLimit);
        }
    }
}
