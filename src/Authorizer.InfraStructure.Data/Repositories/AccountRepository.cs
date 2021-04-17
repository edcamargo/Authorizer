using Authorizer.Domain.Entities;
using Authorizer.Domain.Repositories;
using Authorizer.InfraStructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public IEnumerable<Account> GetAll()
        {
            return _dataContext.Account.AsNoTracking().OrderBy(x => x.AvailableLimit);
        }
    }
}
