
using MiroRabbit.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Transfer.Domain.Events
{
    public class TrasferCreatedEvent :Event
    {
        public long From { get; private set; }  
        public long To { get; private set; }
        public decimal Amount { get; private set; }

        public TrasferCreatedEvent (long from, long to, decimal amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }
    }
}
