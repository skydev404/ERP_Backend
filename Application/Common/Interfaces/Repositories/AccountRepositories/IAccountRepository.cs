using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Models.Account;
using Domain.Entities;
namespace Application.Common.Interfaces.Repositories.AccountRepositories
{
    public interface IAccountRepository : IPlatformRepository<Account, AccountInputModel>
    {
        IQueryable<Account> get();
    }
}
