using Authorizer.Domain.Entities;
using Authorizer.Domain.Interfaces.Repositories;
using Authorizer.InfraStructure.Data.Context;
using System.Linq;

namespace Authorizer.InfraStructure.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public readonly DataContext _dataContext;

        public AccountRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Account Create(Account account)
        {
            _dataContext.Set<Account>().AddRange(account);
            _dataContext.SaveChanges();

            return account;
        }

        public Account FindActiveCard(object obj)
        {
            return _dataContext.Set<Account>().FirstOrDefault(x => x.ActiveCard.Equals(obj));
        }
    }
}
