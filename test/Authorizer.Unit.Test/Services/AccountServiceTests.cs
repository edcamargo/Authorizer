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
    public class AccountServiceTests
    {
        private Mock<IAccountRepository> mockAccountRepository;
        private Mock<ILogger<AccountService>> mockLogger;

        public AccountServiceTests()
        {
            mockAccountRepository = new Mock<IAccountRepository>();
            mockLogger = new Mock<ILogger<AccountService>>();
        }

        [Fact(DisplayName = "Account Execute Init")]
        [Trait("Account", "Execute")]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accountService = new AccountService(mockAccountRepository.Object, mockLogger.Object);
            var account = new Account(true, 100);
            mockAccountRepository.Setup(a => a.Create(It.IsAny<Account>())).Returns(account);

            // Act
            var _command = FakeArchive.AccountInputOne();
            var accountResult = accountService.Execute(_command);
            var activeCardResult =  JsonConvert.SerializeObject(accountResult);

            // Assert
            Assert.NotEmpty(activeCardResult);

            mockAccountRepository.VerifyAll();
        }

        [Fact(DisplayName = "Account Create Valid")]
        [Trait("Account", "Create Account")]
        public void CreateAccount_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var account = new Account(true, 100);
            mockAccountRepository.Setup(a => a.Create(It.IsAny<Account>())).Returns(account);
            var accountService = new AccountService(mockAccountRepository.Object, mockLogger.Object);

            // Act
            var _command = FakeArchive.AccountInputOne();
            var accountDto = JsonConvert.DeserializeObject<RootAccount>(_command);
            var result = accountService.CreateAccount(accountDto);

            // Assert
            Assert.True(result.account.ActiveCard);
            Assert.Equal(100, result.account.AvailableLimit);
            Assert.Empty(result.account.Violations); 

            mockAccountRepository.VerifyAll();
        }

        [Fact(DisplayName = "Account Already Initialized")]
        [Trait("Account", "Account Return")]
        public void AccountReturn_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var account = new Account(true, 100);
            mockAccountRepository.Setup(a => a.Create(It.IsAny<Account>())).Returns(account);
            var accountService = new AccountService(mockAccountRepository.Object, mockLogger.Object);
            
            // Act
            var result = accountService.AccountReturn(account);
            var msgReturn = result.account.Violations[0];

            // Assert
            Assert.True(result.account.ActiveCard);
            Assert.Equal(100, result.account.AvailableLimit);
            Assert.Equal("account-already-initialized", msgReturn);
        }
    }
}
