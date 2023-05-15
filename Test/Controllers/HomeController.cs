using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Test.DAL;
using Test.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context;
        private readonly ILogger _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> CreateUser() => View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUser(Notes model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            context.Notes.Add(model);
            await context.SaveChangesAsync();
            return RedirectToAction("Index","Note");
        }

        public IActionResult Index()
        {
            return View();
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