using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMediate.Features.Consults
{
    public record ConsultDto(long Id, long ClientId, DateTime StartDate, DateTime EndDate);
}
