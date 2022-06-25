using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MakaleWebProject.Filter
{
    public class 
Auth:FilterAttribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filtercontext)
        {       

            if (filtercontext.HttpContext.Session["login"]==null)
            {
                filtercontext.Result = new RedirectResult("/Home/Login");
            }
        }

    }
}