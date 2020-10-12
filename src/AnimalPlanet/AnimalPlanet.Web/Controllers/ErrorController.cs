using System.Diagnostics;

using AnimalPlanet.Models;
using AnimalPlanet.Web.ViewHelpers;

using Microsoft.AspNetCore.Mvc;

namespace AnimalPlanet.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ErrorController()
        {

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Error(ErrorCode errorCode, string modelName)
        {
            if (errorCode == ErrorCode.NotFound)
                return NotFound(modelName);

            ViewData["Title"] = "Error";
            ViewData["Message"] = $"{AnimalPlanetHelpers.GetEnumDescription(errorCode)}";

            return View("HandledError");
        }

        public IActionResult NotFound(string modelName)
        {
            ViewData["Title"] = "Not found";
            ViewData["Message"] = $"{modelName} is not found";

            return View("HandledError");
        }
    }
}
