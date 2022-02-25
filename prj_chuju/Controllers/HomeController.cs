using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prj_chuju.ViewModels;

namespace prj_chuju.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            // 刷新Cookie
            new AccountInfoHelper(Session, Request).updateCookie(Response, Request);

            dbchujuEntities1 db = new dbchujuEntities1();
            HomePageViewModel vm = new HomePageViewModel()
            {
                building = db.buildingdb.OrderByDescending(t => t.建案序號).Take(4).ToList(),
                ActivityOutline = db.ActivityOutline.Take(6).ToList(),
                ActivityContent = db.ActivityContent.Take(6).ToList(),
            };

            

            return View(vm);
        }
    }
}