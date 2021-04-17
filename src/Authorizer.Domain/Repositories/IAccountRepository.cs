using Authorizer.Domain.Entities;
using System.Collections.Generic;

namespace Authorizer.Domain.Repositories
{
    public interface IAccountRepository
    {
        void Create(Account account);

        IEnumerable<Account> GetAll();
    }
}
