using Feat.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Feat.Feature.WeatherGet
{
    public class GetWeather
    {
        public class Query: IRequest<IEnumerable<WeatherForecast>>
        {

        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public class QueryHandler : IRequestHandler<Query, IEnumerable<WeatherForecast>>
        {
            private readonly ILogger<QueryHandler> logger;
            public QueryHandler(ILogger<QueryHandler> logger)
            {
                this.logger = logger;
            }
            public async Task<IEnumerable<WeatherForecast>> Handle(Query request, CancellationToken cancellationToken)
            {
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToList();
            }
        }
    }
}
