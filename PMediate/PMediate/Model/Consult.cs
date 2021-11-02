using PMediate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMediate.Model
{
    public class Consult
    {
        public Consult(long clientId, DateTime startDate, DateTime endDate)
        {
            ClientId = clientId;
            DateRange = DateRange.Create(startDate, endDate).Value;
        }

        private Consult()
        {

        }

        public long Id { get; private set; }
        public long ClientId { get; private set; }
        public DateRange DateRange { get; private set; }

        public void Reschadule(DateTime startDate, DateTime endDate)
        {
            DateRange = DateRange.Create(startDate, endDate).Value;
        }

        public void EnsureAvailability(IAppDbContext context)
        {
            if (context.Consults.Any(x => DateRange.StartDate < x.DateRange.EndDate && x.DateRange.StartDate < DateRange.EndDate))
            {
                throw new Exception("Overlap");
            }
        }
    }
}
