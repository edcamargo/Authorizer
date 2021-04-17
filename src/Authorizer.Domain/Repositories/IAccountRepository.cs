using Authorizer.Domain.Entities;

namespace Authorizer.Domain.Repositories
{
    public interface IAccountRepository
    {
        void Create(Account account);

        Account Find(Account account);
    }
}
