using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Application.Models
{
    public class AccountTransfer
    {
        public long FromAccount { get; set; }
        public long ToAccount { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
