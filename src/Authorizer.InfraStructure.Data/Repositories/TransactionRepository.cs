using Authorizer.Domain.Entities;
using Authorizer.Domain.Interfaces.Repositories;
using Authorizer.InfraStructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Authorizer.InfraStructure.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public readonly DataContext _dataContext;

        public TransactionRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Transaction Create(Transaction transation)
        {
            _dataContext.Set<Transaction>().AddRange(transation);
            _dataContext.SaveChanges();

            return transation;
        }

        public int GetSumCalculater()
        {
            return _dataContext.Set<Transaction>().AsNoTracking().Sum(x => x.Amount);
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _dataContext.Set<Transaction>().AsNoTracking();
        }

        public Transaction GetLastTransaction()
        {
            return _dataContext.Set<Transaction>().OrderByDescending(x => x.Time).FirstOrDefault();
        }
    }
}
