using Authorizer.Domain.Entities;

namespace Authorizer.Domain.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Account Create(Account account);

        Account FindActiveCard();
    }
}
