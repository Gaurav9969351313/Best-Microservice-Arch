using BankingAccountAPI.Context;
using BankingAccountAPI.Interfaces;
using BankingAccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingAccountAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private BankingDbContext _ctx;

        public AccountRepository(BankingDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _ctx.Accounts;
        }
    }
}