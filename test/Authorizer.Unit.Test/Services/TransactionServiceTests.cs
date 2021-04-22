using Authorizer.Domain.Entities;
using Authorizer.Domain.Interfaces.Repositories;
using Authorizer.InfraStructure.Data.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
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

            this.mockTransactionRepository = this.mockRepository.Create<ITransactionRepository>();
            this.mockAccountRepository = this.mockRepository.Create<IAccountRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<TransactionService>>();
        }

        private TransactionService CreateService()
        {
            return new TransactionService(
                this.mockTransactionRepository.Object,
                this.mockAccountRepository.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string command = null;

            // Act
            var result = service.Execute(
                command);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void ValidateInsufficientLimit_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Account account = null;
            Transaction transaction = null;
            bool errorLimit = false;

            // Act
            service.ValidateInsufficientLimit(
                account,
                transaction,
                out errorLimit);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void ValidateSmallInterval_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Account account = null;
            Transaction transaction = null;
            bool errorSmall = false;

            // Act
            service.ValidateSmallInterval(
                account,
                transaction,
                out errorSmall);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
