using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMediate.Features.Consults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PMediate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ConsultController : Controller
    {
        private readonly IMediator _mediator;

        public ConsultController(IMediator mediator) => _mediator = mediator;
        [HttpPost(Name = "CreateConsultRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateConsult.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateConsult.Response>> Create([FromBody] CreateConsult.Request request)
            => await _mediator.Send(request);
    }
}
