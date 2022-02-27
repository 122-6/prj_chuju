using prj_chuju.Models;
using prj_chuju.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prj_chuju.Controllers
{
    public class BuildingIntroController : Controller
    {
        // GET: BuildingIntro
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Page(int? id)
        {
            buildingContentHelper bch = new buildingContentHelper();
            if (id == null || !bch.hasData((int)id))
                return RedirectToAction("Index", "BuildingList");

            AccountInfoMemory userInfo = new AccountInfoHelper(Session, Request).Information;
            new factory_CollectAndViewed().visitBuilding(userInfo.theid, (int)id);

            buildingContentInfo x = bch.getBuildingContentInfoByID((int)id);
            return View(x);
        }

        public ActionResult easyTestPage()
        {
            return View();
        }

        public JsonResult getBuildingInfo()
        {
            int theid = Convert.ToInt32(Request["theid"]);
            buildingContentInfo x = new buildingContentHelper().getBuildingContentInfoByID(theid);
            return Json(x);
        }
    }
}