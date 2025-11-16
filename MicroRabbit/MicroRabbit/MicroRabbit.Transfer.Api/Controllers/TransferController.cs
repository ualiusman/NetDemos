using MicroRabbit.Banking.Domain.Models;
using MicroRabbit.Transfer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbit.Transfer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController : ControllerBase
    {

        private readonly ITransferService transferService;
        private readonly ILogger<TransferController> _logger;

        public TransferController(ILogger<TransferController> logger, ITransferService transferService)
        {
            _logger = logger;
            this.transferService = transferService;
        }

        [HttpGet]
        public ActionResult<TransferLog> Get()
        {
            return Ok(transferService.GetTransferLogs());
        }
    }
}