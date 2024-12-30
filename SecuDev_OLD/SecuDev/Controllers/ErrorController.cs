using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecuDev.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(int ERRCode)
        {

            ViewBag.ERRCode = ERRCode;

            return View();
        }
    }
}