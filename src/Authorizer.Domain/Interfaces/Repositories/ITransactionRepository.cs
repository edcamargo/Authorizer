using Authorizer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Authorizer.Domain.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Transaction Create(Transaction transation);

        int GetSumCalculater();

        IEnumerable<Transaction> GetAll();

        Transaction GetLastTransaction();
        //IQueryable<Transaction> GetLastTransactionMerchant(string LastMerchant);
    }
}
