using prj_chuju.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prj_chuju.Controllers
{
    public class ActivityController : Controller
    {
        
        SqlConnection con;
        // GET: Activity
        public ActionResult Index()
        {
            List<class_ActivityOutline> list = SqlAll();
            return View(list);
        }

   
     
        private List<class_ActivityOutline> SqlAll()
        {
            string dataSQL = "select * from ActivityOutline";
            SqlCommand cmd = methodSQL(dataSQL);
            SqlDataReader reader = cmd.ExecuteReader();
            List<class_ActivityOutline> list = new List<class_ActivityOutline>();
            while (reader.Read())
            {
                class_ActivityOutline x = new class_ActivityOutline()
                {
                    Id = (int)reader["Id"],
                    startDate = (DateTime)reader["startDate"],
                    endDate = (DateTime)reader["endDate"],
                    picture = (string)reader["picture"]
                };
                list.Add(x);
            }
            reader.Close();
            con.Close();
            return list;
        }

        private class_ActivityContent ActivityContent(string Id)
        {
            class_ActivityContent x = default;
            string IdSql = $"select * from ActivityContent where ActivityId = {Id}";

            SqlCommand cmd = methodSQL(IdSql);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                x = new class_ActivityContent()
                {
                    title = (string)reader["title"],
                    content = (string)reader["content"]
                };
            }

            return x;
        }

        private SqlCommand methodSQL(string strSQL)
        {
            con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=testActitvity;Integrated Security=True";
            con.Open();
            return new SqlCommand(strSQL, con);
        }
        
    }
}