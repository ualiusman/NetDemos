using MediatR;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Services;
using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Data.Repository;
using MicroRabbit.Banking.Domain.CommandHandlers;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Infa.Bus;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Data.Repository;
using MicroRabbit.Transfer.Domain.EventHandlers;
using MicroRabbit.Transfer.Domain.Events;
using MicroRabbit.Transfer.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MiroRabbit.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
            service.AddTransient<IEventBus, RabbitMQBus>();

            service.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();

            service.AddTransient<IEventHandler<TrasferCreatedEvent>, TransferEventHandler>();

            service.AddTransient<IAcccountService, AccountService>();
            service.AddTransient<IAccountRepository, AccountRepository>();
            service.AddTransient<BankingDbContext>();

            service.AddTransient<ITransferService, TransferService>();
            service.AddTransient<ITransferRepository, TransferRepository>();
            service.AddTransient<TransferDbContext>();

            service.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
