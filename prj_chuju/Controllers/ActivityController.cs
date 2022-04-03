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
            List<class_ActivityOutline> list;
            int max_page;

            string allSql = "select * from ActivityOutline order by endDate desc offset @Page_row rows fetch next 4 rows only;";
            string all_countSql = "select count(*) from ActivityOutline";
            string soonSql = "select * from ActivityOutline where convert(varchar(10), getdate(), 23) between dateadd(day, -7, startDate) and dateadd(day, -1, startDate) order by endDate desc offset @Page_row rows fetch next 4 rows only;";
            string soon_countSql = "select count(*) from ActivityOutline where getdate() between dateadd(day, -7, startDate) and dateadd(day, -1, startDate)";
            string nowSql = "select * from ActivityOutline where convert(varchar(10), getdate(), 23) between startDate and endDate order by endDate desc offset @Page_row rows fetch next 4 rows only;";
            string now_countSql = "select count(*) from ActivityOutline where getdate() between startDate and endDate";
            string endSql = "select * from ActivityOutline where convert(varchar(10), getdate(), 23) > endDate order by endDate desc offset @Page_row rows fetch next 4 rows only;";
            string end_countSql = "select count(*) from ActivityOutline where getdate() > endDate";

            switch (tag)
            {
                case "即將開始":
                    list = OutlinetSql(soonSql, page);
                    max_page = MaxPage_Sql(soon_countSql);
                    break;
                case "執行中":
                    list = OutlinetSql(nowSql, page);
                    max_page = MaxPage_Sql(now_countSql);
                    break;
                case "已結束":
                    list = OutlinetSql(endSql, page);
                    max_page = MaxPage_Sql(end_countSql);
                    break;
                default:
                    list = OutlinetSql(allSql, page);
                    max_page = MaxPage_Sql(all_countSql);
                    break;
            }
            Tuple<List<class_ActivityOutline>, string, int, int> data = new Tuple<List<class_ActivityOutline>, string, int, int>(list, tag, max_page, page);

            return View(data);
        }

        private List<class_ActivityOutline> OutlinetSql(string strSql, int page)
        {
            SqlCommand cmd = methodSQL(strSql);
            cmd.Parameters.AddWithValue("@Page_row", (page - 1) * 4);
            SqlDataReader reader = cmd.ExecuteReader();
            List<class_ActivityOutline> list = ListSql(reader);

            reader.Close();
            con.Close();

            return list;
        }

        private List<class_ActivityOutline> ListSql(SqlDataReader reader)
        {
            List<class_ActivityOutline> list = new List<class_ActivityOutline>();
            if (reader.HasRows)
            {
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
            }

            return list;
        }

        private int MaxPage_Sql(string strSql)
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
            int row_number = Rows_numberSql(Id);
            int compute_row_number;
            int previous_rows_fetch = 3;
            List<class_ActivityOutline> previouslist;

            switch (row_number)
            {
                case 3:
                    compute_row_number = 0;
                    previous_rows_fetch = 2;
                    break;
                case 2:
                    compute_row_number = 0;
                    previous_rows_fetch = 1;
                    break;
                default:
                    compute_row_number = row_number - 4;
                    break;
            }

            string previous_3data = "select * from ActivityOutline order by endDate desc, Id offset @compute_row_number rows fetch next @previous_rows_fetch rows only;";
            string next_3data = "select * from ActivityOutline order by endDate desc, Id offset @compute_row_number rows fetch next 3 rows only;";

            if (row_number == 1)
            {
                previouslist = new List<class_ActivityOutline>();
            }

            else
            {
                previouslist = OtherActivitySql(previous_3data, compute_row_number, previous_rows_fetch);
            }

            List<class_ActivityOutline> nextlist = OtherActivitySql(next_3data, row_number, previous_rows_fetch);

            Tuple<class_ActivityContent, List<class_ActivityOutline>, List<class_ActivityOutline>> data = new Tuple<class_ActivityContent, List<class_ActivityOutline>, List<class_ActivityOutline>>(content, previouslist, nextlist);

            return View(data);
        }
        private class_ActivityContent QueryContentById(int Id)
        {
            class_ActivityContent x = new class_ActivityContent();
            string IdSql = "select ActivityId, title, content from ActivityContent where ActivityId = @Id";

            SqlCommand cmd = methodSQL(IdSql);
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    x.Id = (int)reader["ActivityId"];
                    x.title = (string)reader["title"];
                    x.content = (string)reader["content"];
                };
            }

            reader.Close();
            con.Close();

            return x;
        }

        private int Rows_numberSql(int Id)
        {
            string row_numberSql = "with ActivityOutline as (select ROW_NUMBER() over (order by endDate desc) as ROW_ID, * from dbo.ActivityOutline) select * from ActivityOutline where Id = @Id";
            SqlCommand cmd = methodSQL(row_numberSql);
            cmd.Parameters.AddWithValue("@Id", Id);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private List<class_ActivityOutline> OtherActivitySql(string strSql, int row_number, int previous_rows_fetch)
        {
            SqlCommand cmd = methodSQL(strSql);
            cmd.Parameters.AddWithValue("@compute_row_number", row_number);
            cmd.Parameters.AddWithValue("@previous_rows_fetch", previous_rows_fetch);
            SqlDataReader reader = cmd.ExecuteReader();

            List<class_ActivityOutline> list = ListSql(reader);

            reader.Close();
            con.Close();

            return list;
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