using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace SecuDev.Filter
{
    public class SessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string session = filterContext.HttpContext.Session.GetString("UID");
            if (session == null)
            {
                filterContext.Result = new RedirectResult("/?alertType=Session");
            }

        }
    }
}