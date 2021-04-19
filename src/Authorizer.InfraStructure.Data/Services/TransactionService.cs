using Authorizer.Domain.Entities;
using Authorizer.Domain.Interfaces.Repositories;
using Authorizer.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Authorizer.InfraStructure.Data.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(ITransactionRepository transactionRepository,
                                  IAccountRepository accountRepository,
                                  ILogger<TransactionService> logger)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public object Execute(string command)
        {
            _logger.LogInformation("Init Transact!");

            var transactionDto = JsonConvert.DeserializeObject<RootTransaction>(command);
            var transaction = new Transaction(transactionDto.transaction.Merchant, 
                                              transactionDto.transaction.Amount, 
                                              transactionDto.transaction.Time);
            
            var existsActiveCard = _accountRepository.FindActiveCard();

            if (existsActiveCard == null)
                return JsonConvert.SerializeObject("account-not-initialized");

            if (!existsActiveCard.ActiveCard)
                return JsonConvert.SerializeObject("card-not-active");

            ValidateInsufficientLimit(existsActiveCard, transaction, out bool errorLimit);
            ValidateSmallInterval(existsActiveCard, transaction, out bool errorSmall);

            if(!errorLimit && !errorSmall)
                _transactionRepository.Create(transaction);

            return JsonConvert.SerializeObject(existsActiveCard);
        }

        public void ValidateInsufficientLimit(Account account, Transaction transaction, out bool errorLimit)
        {
            errorLimit = false;
            var sumCalculater = _transactionRepository.GetSumCalculater();
            var sumTransact = sumCalculater + transaction.Amount;

            if (sumTransact > account.AvailableLimit)
            {
                errorLimit = true;
                account.ThrowsViolation("insufficient-limit");
            }
        }

        public void ValidateSmallInterval(Account account, Transaction transaction, out bool errorSmall)
        {
            errorSmall = false;
            var lastTransaction = _transactionRepository.GetLastTransaction();
            if (lastTransaction != null)
            {
                var diffTransaction = transaction.Time.Subtract(lastTransaction.Time);

                if (diffTransaction.TotalMinutes < 3)
                {
                    errorSmall = true;
                    account.ThrowsViolation("high-frequency-small-interval");
                }
            }
        }
    }
}
