using BankingAccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingAccountAPI.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
        void Transfer(AccountTransfer accountTransfer);
    }
}