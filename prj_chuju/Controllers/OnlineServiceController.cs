using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prj_chuju.Controllers
{
    public class OnlineServiceController : Controller
    {
        // GET: OnlineService
        public ActionResult Service()
        {
            if (Session["permission"] == null || Session["permission"].ToString() != "admin")
                return RedirectToAction("Index", "Home");
            return PartialView();
        }
    }
}