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
        public ActionResult Index(string tag, int page = 1)
        {
            List<class_ActivityOutline> list = default;
            int count = default;

            string allSql = $"select * from ActivityOutline order by endDate desc offset {(page - 1) * 4} rows fetch next 4 rows only;";
            string all_countSql = "select count(*) from ActivityOutline";
            string soonSql = $"select * from ActivityOutline where getdate() between dateadd(day, -7, startDate) and dateadd(day, -1, startDate) order by endDate desc offset {(page - 1) * 4} rows fetch next 4 rows only;";
            string soon_countSql = "select count(*) from ActivityOutline where getdate() between dateadd(day, -7, startDate) and dateadd(day, -1, startDate)";
            string nowSql = $"select * from ActivityOutline where getdate() between startDate and endDate order by endDate desc offset {(page - 1) * 4} rows fetch next 4 rows only;";
            string now_countSql = "select count(*) from ActivityOutline where getdate() between startDate and endDate";
            string endSql = $"select * from ActivityOutline where getdate() > endDate order by endDate desc offset {(page - 1) * 4} rows fetch next 4 rows only;";
            string end_countSql = "select count(*) from ActivityOutline where getdate() > endDate";

            switch (tag)
            {
                case "即將開始":
                    list = ListSql(soonSql);
                    count = CountSql(soon_countSql);
                    break;
                case "執行中":
                    list = ListSql(nowSql);
                    count = CountSql(now_countSql);
                    break;
                case "已結束":
                    list = ListSql(endSql);
                    count = CountSql(end_countSql);
                    break;
                default:
                    list = ListSql(allSql);
                    count = CountSql(all_countSql);
                    break;
            }
            Tuple<List<class_ActivityOutline>, string, int, int> data = new Tuple<List<class_ActivityOutline>, string, int, int>(list, tag, count, page);

            return View(data);
        }

        private List<class_ActivityOutline> ListSql(string strSql)
        {
            SqlCommand cmd = methodSQL(strSql);
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

        private int CountSql(string strSql)
        {
            int count = 1;
            SqlCommand cmd = methodSQL(strSql);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() && (int)reader[0] != 0)
            {
                count = (int)reader[0] / 4;
                if ((int)reader[0] % 4 != 0)
                {
                    count++;
                }
            }
            reader.Close();
            con.Close();

            return (count);
        }

        public ActionResult ActivityContent(int Id)
        {
            var content = QueryContentById(Id);
            return View(content);
        }
        private class_ActivityContent QueryContentById(int Id)
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
            reader.Close();
            con.Close();

            return x;
        }

        private SqlCommand methodSQL(string strSQL)
        {
            con = new SqlConnection();
            con.ConnectionString = @"Data Source=chujudbserver.database.windows.net;Initial Catalog=dbchuju;Persist Security Info=True;User ID=chujuas;Password=P@ssw0rd-chuju;MultipleActiveResultSets=True;Application Name=EntityFramework";
            con.Open();
            return new SqlCommand(strSQL, con);
        }

    }
}