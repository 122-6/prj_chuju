using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prj_chuju.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            // 刷新Cookie
            HttpCookie x = new HttpCookie("userInfo");
            if (x != null)
            {
                x.Expires = DateTime.Now.AddDays(14);
                Response.Cookies.Add(x);
            }

            return View();
        }
    }
}