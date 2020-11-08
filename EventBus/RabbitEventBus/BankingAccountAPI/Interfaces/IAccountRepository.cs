using BankingAccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingAccountAPI.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();
    }
}