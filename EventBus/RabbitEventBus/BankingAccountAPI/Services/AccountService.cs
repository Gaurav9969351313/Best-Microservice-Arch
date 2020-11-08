using BankingAccountAPI.Interfaces;
using BankingAccountAPI.Models;
using BankingAccountAPI.Commands;
using BankingAccountAPI.Interfaces;
using BankingAccountAPI.Models;
using CLBRabbitMQEventBus.Bus;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingAccountAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventBus _bus;

        public AccountService(IAccountRepository accountRepository, IEventBus bus)
        {
            _accountRepository = accountRepository;
            _bus = bus;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public void Transfer(AccountTransfer accountTransfer)
        {
            var createTransferCommand = new CreateTransferCommand(
                    accountTransfer.FromAccount,
                    accountTransfer.ToAccount,
                    accountTransfer.TransferAmount
                );

            _bus.SendCommand(createTransferCommand);
        }
    }
}