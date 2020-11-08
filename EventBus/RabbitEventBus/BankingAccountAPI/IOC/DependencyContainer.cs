using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using BankingAccountAPI.Interfaces;
using BankingAccountAPI.Services;
using BankingAccountAPI.Context;
using BankingAccountAPI.Repository;
using BankingAccountAPI.CommandHandlers;
using BankingAccountAPI.Commands;
using BankingAccountAPI.Interfaces;
using CLBRabbitMQEventBus.Infra;
using CLBRabbitMQEventBus.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace BankingAccountAPI.IOC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            });

            // Subscriptions
            // services.AddTransient<TransferEventHandler>();

            // Domain Events
            // services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

            //Domain Banking Commands
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();

            //Application Services
            services.AddTransient<IAccountService, AccountService>();

            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();

            //  services.AddTransient<ITransferService, TransferService>();
            //  services.AddTransient<ITransferRepository, TransferRepository>();
            //  services.AddTransient<TransferDbContext>();
        }
    }
}