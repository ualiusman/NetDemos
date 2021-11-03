using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DockerApp.Models;
using Microsoft.Extensions.Configuration;

namespace DockerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository repository;
        private string message;

        public HomeController(ILogger<HomeController> logger, IRepository repository, IConfiguration config)
        {
            _logger = logger;
            this.repository = repository;
            message = config["MESSAGE"] ?? "Essential Docker";
        }

        public IActionResult Index()
        {
            ViewBag.Message = message;
            return View(repository.Products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
