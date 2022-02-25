using prj_chuju.Models;
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
            List<class_buildingCase> list = new List<class_buildingCase>();

            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd;
            SqlDataReader reader;

            con.Open();
            cmd = new SqlCommand("select * from buildingdb", con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                class_buildingCase bc = new class_buildingCase(reader);
                list.Add(bc);
            }
            con.Close();

            return View(list);
        }

        public JsonResult getBuildingBySelector()
        {
            List<class_buildingCase> List = new factory_buildingCase().getBuildingBySelector(Request);
            return Json(List);
        }
    }
}