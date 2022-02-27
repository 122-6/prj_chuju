using prj_chuju.Models;
using prj_chuju.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prj_chuju.Controllers
{
    public class BuildingCaseController : Controller
    {
        
        static SqlConnectionStringBuilder myBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "chujudbserver.database.windows.net",
            InitialCatalog = "dbchuju",
            UserID = "chujuas",
            Password = "P@ssw0rd-chuju",
            AsynchronousProcessing = true,
        };
        static string conStr = myBuilder.ToString();


        // GET: BuildingList


        public ActionResult Index()
        {
            AccountInfoMemory userInfo = new AccountInfoHelper(Session, Request).Information;

            ViewBag.userID = userInfo.theid;
            ViewBag.region = Request["region"];
            ViewBag.area = Request["area"];
            ViewBag.rooms = Request["rooms"];
            ViewBag.status = Request["status"];
            ViewBag.tags = Request["tags"];

            return View();
        }

        public JsonResult getBuildingBySelector()
        {
            List<class_buildingCase> List = new factory_buildingCase().getBuildingBySelector(Request);
            return Json(List);
        }

        public void collect()
        {
            int userID = Request["userID"] != null ? Convert.ToInt32(Request["userID"]) : -1;
            int buildingID = Request["buildingID"] != null ? Convert.ToInt32(Request["buildingID"]) : -1;

            new factory_CollectAndViewed().collectBuilding(userID, buildingID);
        }
    }
}