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
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly Mock<ILogger<AccountService>> _mockLogger;

        public AccountServiceTests()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockLogger = new Mock<ILogger<AccountService>>();
        }

        [Fact(DisplayName = "Account Execute Init")]
        [Trait("Account", "Execute")]
        public void ExecuteStateUnderTestExpectedBehavior()
        {
            // Arrange
            var accountService = new AccountService(_mockAccountRepository.Object, _mockLogger.Object);
            var account = new Account(true, 100);
            _mockAccountRepository.Setup(a => a.Create(It.IsAny<Account>())).Returns(account);

            // Act
            var _command = FakeArchive.AccountInputOne();
            var accountResult = accountService.Execute(_command);
            var activeCardResult =  JsonConvert.SerializeObject(accountResult);

            // Assert
            Assert.NotEmpty(activeCardResult);

            _mockAccountRepository.VerifyAll();
        }

        [Fact(DisplayName = "Account Create Valid Success")]
        [Trait("Account", "Create Account")]
        public void CreateAccountStateUnderTestExpectedBehavior()
        {
            // Arrange
            var account = new Account(true, 100);
            _mockAccountRepository.Setup(a => a.Create(It.IsAny<Account>())).Returns(account);
            var accountService = new AccountService(_mockAccountRepository.Object, _mockLogger.Object);

            // Act
            var _command = FakeArchive.AccountInputOne();
            var accountDto = JsonConvert.DeserializeObject<RootAccount>(_command);
            var result = accountService.CreateAccount(accountDto);

            // Assert
            Assert.True(result.account.ActiveCard);
            Assert.Equal(100, result.account.AvailableLimit);
            Assert.Empty(result.account.Violations); 

            _mockAccountRepository.VerifyAll();
        }

        [Fact(DisplayName = "Account Already Initialized")]
        [Trait("Account", "Account Return")]
        public void AccountReturnStateUnderTestExpectedBehavior()
        {
            // Arrange
            var account = new Account(true, 100);
            _mockAccountRepository.Setup(a => a.Create(It.IsAny<Account>())).Returns(account);
            var accountService = new AccountService(_mockAccountRepository.Object, _mockLogger.Object);

            // Act
            var result = accountService.AccountReturn(account);
            var msgReturn = result.account.Violations[0];
            var expected = "account-already-initialized";

            // Assert
            Assert.True(result.account.ActiveCard);
            Assert.Equal(100, result.account.AvailableLimit);
            Assert.Equal(expected, msgReturn);
        }
    }
}
