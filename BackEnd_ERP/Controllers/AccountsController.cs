using Application.Common.Interfaces.Repositories.AccountRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/Accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private IAccountRepository AccountRepo;

        public AccountsController(IAccountRepository accountRepository)
        {
            AccountRepo = accountRepository;
        }

        [HttpGet]
        public /*IEnumerable<Account>*/ string Get()
        {
            return "hi";//AccountRepo.get();
        }

        [HttpGet("hydra")]
        public string Get8539()
        {
            //Use to test nLog
            //throw new Exception("Hail, Hydra!");
            return "Hail, Hydra!";
        }
    }
}
