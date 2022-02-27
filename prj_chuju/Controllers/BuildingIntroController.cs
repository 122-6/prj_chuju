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
            // 無建案內容則跳回建案列表
            buildingContentHelper bch = new buildingContentHelper();
            if (id == null || !bch.hasData((int)id))
                return RedirectToAction("Index", "BuildingList");
            // 取得建案內容
            buildingContentInfo x = bch.getBuildingContentInfoByID((int)id);

            // 加入瀏覽歷史
            AccountInfoMemory logInfo = new AccountInfoHelper(Session, Request).Information;
            new factory_CollectAndViewed().visitBuilding(logInfo.theid, (int)id);

            // 取得會員資料
            AccountInfoPageViewModel user = new AccountInfoPageViewModel(logInfo.theid);
            ViewBag.buildingID = (int)id;
            ViewBag.userID = user.accountInfo.id;
            ViewBag.userName = logInfo.theid > 0 ? user.accountInfo.userName : "";
            ViewBag.phone = logInfo.theid > 0 ? user.accountInfo.cellphone : "";
            ViewBag.email = logInfo.theid > 0 ? user.accountInfo.email : "";
            ViewBag.cityID = user.regionInfo.cityID;
            ViewBag.regionID = user.regionInfo.regionID;
            
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

        public void bookCase()
        {
            new factory_CollectAndViewed().bookCase(Request);
        }
    }
}