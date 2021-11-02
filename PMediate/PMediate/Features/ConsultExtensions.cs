using PMediate.Features.Consults;
using PMediate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMediate.Features
{
    public static class ConsultExtensions
    {
        public static ConsultDto ToDto(this Consult consult)
            => new(consult.Id, consult.ClientId, consult.DateRange.StartDate, consult.DateRange.EndDate);
    }
}
