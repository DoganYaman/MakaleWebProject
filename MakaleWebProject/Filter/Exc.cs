﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakaleWebProject.Filter
{
    public class Exc:FilterAttribute,IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Controller.TempData["Error"] = filterContext.Exception;

            filterContext.ExceptionHandled = true;

            filterContext.Result = new RedirectResult("/Home/HataSayfasi");
        }
    }
}