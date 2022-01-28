using prj_chuju.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prj_chuju.Controllers
{
    public class BrandArticleController : Controller
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

        // GET: BrandArticle
        public ActionResult Index()
        {
            List<class_articleOutline> list = new List<class_articleOutline>();

            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd;
            SqlDataReader reader;

            con.Open();
            cmd = new SqlCommand("select * from articleOutline",con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                class_articleOutline ao = new class_articleOutline(reader);
                list.Add(ao);
            }
            con.Close();

            return View(list);
        }
    }
}