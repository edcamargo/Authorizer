using Authorizer.Domain.Entities;
using Authorizer.Domain.Repositories;
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

        public void Create(Account account)
        {
            _dataContext.Account.Add(account);
            _dataContext.SaveChanges();
        }

        public Account Find(Account account)
        {
            return _dataContext.Account.FirstOrDefault(x => x.ActiveCard.Equals(account.ActiveCard) 
                                                         && x.AvailableLimit.Equals(account.AvailableLimit));
        }
    }
}
