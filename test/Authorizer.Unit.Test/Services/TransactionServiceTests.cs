using Authorizer.Domain.Entities;
using Authorizer.Domain.Interfaces.Repositories;
using Authorizer.InfraStructure.Data.Services;
using Authorizer.Unit.Test.JsonFake;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Authorizer.Unit.Test.Services
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionRepository> _mockTransactionRepository;
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly Mock<ILogger<TransactionService>> _mockLogger;

        public TransactionServiceTests()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockTransactionRepository = new Mock<ITransactionRepository>();
            _mockLogger = new Mock<ILogger<TransactionService>>();
        }

        [Fact(DisplayName = "Account Not Initialized")]
        [Trait("Transaction", "Create Transact")]
        public void Should_ExecuteAccountNotInitialized_ReturnTrue()
        {
            // Arrange
            var command = FakeArchive.TransactionInputOne();

            // Value transaction - $ 90.00 
            var jsonTransaction = JsonConvert.DeserializeObject<RootTransaction>(command);
            var transaction = new Transaction(jsonTransaction.transaction.Merchant,
                                              jsonTransaction.transaction.Amount,
                                              jsonTransaction.transaction.Time);

            _mockAccountRepository.Setup(a => a.FindActiveCard()).Returns((Account)null);
            _mockTransactionRepository.Setup(a => a.GetLastTransaction()).Returns(transaction);

            // Act
            var transactionService = new TransactionService(_mockTransactionRepository.Object, _mockAccountRepository.Object, _mockLogger.Object);
            var result = transactionService.Execute(command);
            var expected = "account-not-initialized";

            // Assert
            Assert.Contains(expected, result.ToString());
        }

        [Fact(DisplayName = "Account Card Not Active")]
        [Trait("Transaction", "Create Transact")]
        public void Should_ExecuteAccountCardNotActive_ReturnTrue()
        {
            // Arrange
            var command = FakeArchive.TransactionInputOne();

            // Value transaction - $ 90.00 
            var jsonTransaction = JsonConvert.DeserializeObject<RootTransaction>(command);
            var transaction = new Transaction(jsonTransaction.transaction.Merchant,
                                              jsonTransaction.transaction.Amount,
                                              jsonTransaction.transaction.Time);

            var account = new Account(false, 100);
            _mockAccountRepository.Setup(a => a.FindActiveCard()).Returns(account);
            _mockTransactionRepository.Setup(a => a.GetLastTransaction()).Returns(transaction);

            // Act
            var transactionService = new TransactionService(_mockTransactionRepository.Object, _mockAccountRepository.Object, _mockLogger.Object);
            var result = transactionService.Execute(command);
            var expected = "card-not-active";

            // Assert
            Assert.Contains(expected, result.ToString());
        }

        [Fact(DisplayName = "Insufficient Limit")]
        [Trait("Transaction", "Create Transact")]
        public void Should_ValidateInsufficientLimit_ReturnTrue()
        {
            // Arrange
            var account = new Account(true, 100);
            var fakeTransaction = FakeArchive.TransactionInputOne();

            // Value transaction - $ 90.00 
            var jsonTransaction = JsonConvert.DeserializeObject<RootTransaction>(fakeTransaction);
            var transaction = new Transaction(jsonTransaction.transaction.Merchant,
                                              jsonTransaction.transaction.Amount,
                                              jsonTransaction.transaction.Time);

            _mockAccountRepository.Setup(a => a.Create(It.IsAny<Account>())).Returns(account);
            _mockTransactionRepository.Setup(a => a.GetLastTransaction()).Returns(transaction);
            _mockTransactionRepository.Setup(a => a.GetSumCalculater()).Returns(100);

            // Act
            var transactionService = new TransactionService(_mockTransactionRepository.Object, _mockAccountRepository.Object, _mockLogger.Object);
            transactionService.ValidateInsufficientLimit(account, transaction, out bool errorLimit);

            // Assert
            Assert.True(errorLimit);
        }

        [Fact(DisplayName = "Figh Frequency Small Interval")]
        [Trait("Transaction", "Create Transact")]
        public void ValidateSmallInterval_StateUnderTest_ExpectedBehaviorFail()
        {
            // Arrange
            var account = new Account(true, 100);
            var fakeTransaction = FakeArchive.TransactionInputOne();
            var jsonTransaction = JsonConvert.DeserializeObject<RootTransaction>(fakeTransaction);
            var transaction = new Transaction(jsonTransaction.transaction.Merchant,
                                              jsonTransaction.transaction.Amount,
                                              jsonTransaction.transaction.Time);

            _mockAccountRepository.Setup(a => a.Create(It.IsAny<Account>())).Returns(account);
            _mockTransactionRepository.Setup(a => a.GetLastTransaction()).Returns(transaction);

            // Act
            var transactionService = new TransactionService(_mockTransactionRepository.Object, _mockAccountRepository.Object, _mockLogger.Object);
            transactionService.ValidateSmallInterval(account, transaction, out bool errorSmall);

            // Assert
            Assert.True(errorSmall);
        }
    }
}
