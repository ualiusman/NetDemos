using MediatR;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Events;
using MiroRabbit.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Domain.CommandHandlers
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private IEventBus _bus;

        public TransferCommandHandler(IEventBus eventBus)
        {
            _bus = eventBus;
        }
        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            _bus.Publish(new TrasferCreatedEvent(request.From, request.To, request.Amount));
            return Task.FromResult(true);
        }
    }
}
