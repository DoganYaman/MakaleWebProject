using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Makale.Entities;

namespace MakaleWebProject.Filter
{
    public class AuthAdmin:FilterAttribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filtercontext)
        {
            Kullanici user= filtercontext.HttpContext.Session["login"] as Kullanici;

            if (user != null && user.Admin==false)
            {
                filtercontext.Result = new RedirectResult("/Home/YetkisizErisim");
            }
        }
    }
}