using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using TransferSubAPI.Interfaces;
using TransferSubAPI.Services;
using TransferSubAPI.Context;
using TransferSubAPI.Repository;
using TransferSubAPI.Interfaces;
using TransferSubAPI.EventHandlers;
using TransferSubAPI.Events;

using CLBRabbitMQEventBus.Infra;
using CLBRabbitMQEventBus.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace TransferSubAPI.IOC
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
            services.AddTransient<TransferEventHandler>();

            // Domain Events
            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

            services.AddTransient<ITransferService, TransferService>();
            services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddTransient<TransferDbContext>();
        }
    }
}