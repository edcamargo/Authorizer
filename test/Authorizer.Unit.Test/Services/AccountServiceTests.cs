using Authorizer.Domain.Entities;
using Authorizer.Domain.Interfaces.Repositories;
using Authorizer.InfraStructure.Data.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Authorizer.Unit.Test.Services
{
    public class AccountServiceTests
    {
        

        //public AccountServiceTests()
        //{
        //    this.mockRepository = new MockRepository(MockBehavior.Strict);

        //    this.mockAccountRepository = this.mockRepository.Create<IAccountRepository>();
        //    this.mockLogger = this.mockRepository.Create<ILogger<AccountService>>();
        //}

        //private AccountService CreateService()
        //{
        //    return new AccountService(
        //        this.mockAccountRepository.Object,
        //        this.mockLogger.Object);
        //}

        //[Fact]
        //public void Execute_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var service = this.CreateService();
        //    string command = null;

        //    // Act
        //    var result = service.Execute(
        //        command);

        //    // Assert
        //    Assert.True(false);
        //    this.mockRepository.VerifyAll();
        //}

        [Fact]
        public void CreateAccount_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var account = new Account(true, 100);

            Mock<IAccountRepository> mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(a => a.Create(It.IsAny<Account>())).Returns(account);

            Mock<ILogger<AccountService>> mockLogger = new Mock<ILogger<AccountService>>();

            // Act
            var _command = @"{""account"": {""active-card"": true, ""available-limit"": 200}}";
            var accountDto = JsonConvert.DeserializeObject<RootAccount>(_command);
            var accountService = new AccountService(mockAccountRepository.Object, mockLogger.Object);

            // Act
            var result = accountService.CreateAccount(accountDto);

            // Assert
            Assert.True(result.account.ActiveCard);
            Assert.Equal(100, result.account.AvailableLimit);

            mockAccountRepository.VerifyAll();
        }

        //[Fact]
        //public void ReturnAccount_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var service = this.CreateService();
        //    Account account = null;

        //    // Act
        //    var result = service.ReturnAccount(
        //        account);

        //    // Assert
        //    Assert.True(false);
        //    this.mockRepository.VerifyAll();
        //}
    }
}
