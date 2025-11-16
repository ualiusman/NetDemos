
using MicroRabbit.Banking.Domain.Models;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Domain.Interfaces;
using MiroRabbit.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Application.Services
{
    public class TransferService : ITransferService
    {
        private ITransferRepository transferRepository;
        private IEventBus eventBus;

        public TransferService(ITransferRepository transferRepository, IEventBus eventBus)
        {
            this.transferRepository = transferRepository;
            this.eventBus = eventBus;
        }
        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return transferRepository.GetTransferLogs();
        }

       
    }
}
