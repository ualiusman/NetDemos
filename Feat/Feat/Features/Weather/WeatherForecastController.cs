using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feat.Feature.WeatherGet;
using Feat.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Feat.Features.Weather
{
    [Route("weather")]
    public class WeatherForecastController : Controller
    {
       

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IMediator mediator )
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await mediator.Send(new GetWeather.Query());
        }
    }
}
