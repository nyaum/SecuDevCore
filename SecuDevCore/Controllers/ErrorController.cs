using Microsoft.AspNetCore.Mvc;

namespace SecuDevCore.Controllers
{
    public class ErrorController : Controller
    {

        [Route("/Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            ViewBag.ERRCode = statusCode;

            return View();  //Index.html located in wwwroot folder
        }
    }
}
