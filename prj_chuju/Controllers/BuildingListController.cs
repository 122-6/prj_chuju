using prj_chuju.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prj_chuju.Controllers
{
    public class BuildingListController : Controller
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
            List<class_buildingList> list = new List<class_buildingList>();

            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd;
            SqlDataReader reader;

            con.Open();
            cmd = new SqlCommand("select * from buildingdb", con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                class_buildingList bl  = new class_buildingList(reader);
                list.Add(bl);
            }
            con.Close();

            return View(list);
        }
    }
}