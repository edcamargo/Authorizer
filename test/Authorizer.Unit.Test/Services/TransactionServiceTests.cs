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
        private MockRepository mockRepository;

        private Mock<ITransactionRepository> mockTransactionRepository;
        private Mock<IAccountRepository> mockAccountRepository;
        private Mock<ILogger<TransactionService>> mockLogger;

        public TransactionServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockAccountRepository = this.mockRepository.Create<IAccountRepository>();
            this.mockTransactionRepository = this.mockRepository.Create<ITransactionRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<TransactionService>>();
        }

        [Fact(DisplayName = "Account Not Initialized")]
        [Trait("Transaction", "Create Transact")]
        public void Should_ExecuteAccountNotInitialized_ReturnTrue()
        {
            var account = new Account(false, 100);
            var fakeTransaction = FakeArchive.TransactionInputOne();

            var tootTransaction = new RootTransaction()
            {
                transaction = new Transaction()
            };

            // Value transaction - $ 90.00 
            var jsonTransaction = JsonConvert.DeserializeObject<RootTransaction>(fakeTransaction);
            var transaction = new Transaction(jsonTransaction.transaction.Merchant,
                                              jsonTransaction.transaction.Amount,
                                              jsonTransaction.transaction.Time);

            mockAccountRepository.Setup(a => a.Create(It.IsAny<Account>())).Returns(account);
        }

        [Fact(DisplayName = "Insufficient Limit")]
        [Trait("Transaction", "Create Transact")]
        public void Should_ValidateInsufficientLimit_ReturnTrue()
        {
            // Arrange
            var account = new Account(true, 100);
            var fakeTransaction = FakeArchive.TransactionInputOne();

            var tootTransaction = new RootTransaction()
            {
                transaction = new Transaction()
            };

            // Value transaction - $ 90.00 
            var jsonTransaction = JsonConvert.DeserializeObject<RootTransaction>(fakeTransaction);
            var transaction = new Transaction(jsonTransaction.transaction.Merchant,
                                              jsonTransaction.transaction.Amount,
                                              jsonTransaction.transaction.Time);

            mockAccountRepository.Setup(a => a.Create(It.IsAny<Account>())).Returns(account);
            mockTransactionRepository.Setup(a => a.GetLastTransaction()).Returns(transaction);
            mockTransactionRepository.Setup(a => a.GetSumCalculater()).Returns(100);

            // Act
            var transactionService = new TransactionService(mockTransactionRepository.Object, mockAccountRepository.Object, mockLogger.Object);
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

            var tootTransaction = new RootTransaction()
            {
                transaction = new Transaction()
            };

            var jsonTransaction = JsonConvert.DeserializeObject<RootTransaction>(fakeTransaction);

            var transaction = new Transaction(jsonTransaction.transaction.Merchant,
                                              jsonTransaction.transaction.Amount,
                                              jsonTransaction.transaction.Time);

            mockAccountRepository.Setup(a => a.Create(It.IsAny<Account>())).Returns(account);
            mockTransactionRepository.Setup(a => a.GetLastTransaction()).Returns(transaction);

            var transactionService = new TransactionService(mockTransactionRepository.Object, mockAccountRepository.Object, mockLogger.Object);

            // Act
            transactionService.ValidateSmallInterval(account, transaction, out bool errorSmall);

            // Assert
            Assert.True(errorSmall);
        }
    }
}
