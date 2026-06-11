using Microsoft.AspNetCore.Mvc;

namespace FinalExam.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Error/{404}")]
        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
