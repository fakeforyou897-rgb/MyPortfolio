using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models.ViewModels;
using System.Diagnostics;

namespace MyPortfolio.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
