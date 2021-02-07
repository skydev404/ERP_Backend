using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces.DbContexts;
using Application.Common.Interfaces.Repositories.AccountRepositories;
using Domain.Entities;

namespace Infrastructure.Repositories.AccountRepositories
{
    public class AccountRepository : IAccountRepository
    {
        private ITenantDBContext Db;

        public AccountRepository(ITenantDBContext _context)
        {
            Db = _context;
        }

        public IQueryable<Account> get()
        {
            return Db.Accounts;
        }

        public Task<Account> Select(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> Select(IEnumerable<Guid> ids = null)
        {
            throw new NotImplementedException();
        }
    }
}
