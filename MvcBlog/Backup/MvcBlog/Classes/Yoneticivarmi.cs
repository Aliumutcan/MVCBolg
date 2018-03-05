using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlog.Classes
{
    public class Yoneticivarmi : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["yazar"] != null)
            {
                return true;
            }
            else
            {
                httpContext.Response.Redirect("/Yonetim/YonetimGiris");
                return false;
            }

        }
    }
}