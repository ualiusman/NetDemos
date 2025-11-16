using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Application.Services;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbit.Banking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankingController : ControllerBase
    {
        private readonly IAcccountService accountService;

        private readonly ILogger<BankingController> _logger;

        public BankingController(ILogger<BankingController> logger, IAcccountService service)
        {
            _logger = logger;
            accountService = service;
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            return Ok(accountService.GetAccounts());
        }

        [HttpPost]
        public  IActionResult Post(AccountTransfer accountTransfer)
        {
            accountService.TransferFunds(accountTransfer);
            return  Ok(accountTransfer);
        }
    }
}