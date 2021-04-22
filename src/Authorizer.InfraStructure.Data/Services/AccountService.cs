using Authorizer.Domain.Entities;
using Authorizer.Domain.Interfaces.Repositories;
using Authorizer.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Authorizer.InfraStructure.Data.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IAccountRepository accountRepository, ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public object Execute(string command)
        {
            _logger.LogInformation("Init Create Account!");

            var rootAccount = new RootAccount();
            var accountDto = JsonConvert.DeserializeObject<RootAccount>(command);
            var existsActiveCard = _accountRepository.FindActiveCard();

            if (existsActiveCard == null)
                rootAccount = CreateAccount(accountDto);
            
            if (existsActiveCard != null)
                rootAccount = AccountReturn(existsActiveCard);

            return JsonConvert.SerializeObject(rootAccount);
        }
        
        public RootAccount CreateAccount(RootAccount rootAccount)
        {
            var account = new Account(rootAccount.account.ActiveCard, rootAccount.account.AvailableLimit);
            var accountCreated = _accountRepository.Create(account);

            var _rootAccount = new RootAccount()
            {
                account = new Account(accountCreated.ActiveCard, accountCreated.AvailableLimit)
            };

            return _rootAccount;
        }

        public RootAccount AccountReturn(Account account)
        {
            var _rootAccount = new RootAccount()
            {
                account = new Account(account.ActiveCard, account.AvailableLimit)
            };

            _rootAccount.account.ThrowsViolation("account-already-initialized");

            return _rootAccount;
        }
    }
}
