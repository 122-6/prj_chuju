using prj_chuju.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prj_chuju.Controllers
{
    public class ActivityController : Controller
    {
        // GET: Activity
        SqlConnection con;
        List<classActivityOutline> list;
        int maxPage;
        private class QueryAllSql
        {
            public string offsetSql = "select * from ActivityOutline order by endDate desc offset @Page_row rows fetch next 4 rows only;";
            public string countSql = "select count(*) from ActivityOutline";
        }

        private class QuerySoonSql
        {
           public string offsetSql = "select * from ActivityOutline where convert(varchar(10), getdate(), 23) between dateadd(day, -7, startDate) and dateadd(day, -1, startDate) order by endDate desc offset @Page_row rows fetch next 4 rows only;";
           public string countSql = "select count(*) from ActivityOutline where getdate() between dateadd(day, -7, startDate) and dateadd(day, -1, startDate)";
        }

        private class QueryNowSql
        {
            public string offsetSql = "select * from ActivityOutline where convert(varchar(10), getdate(), 23) between startDate and endDate order by endDate desc offset @Page_row rows fetch next 4 rows only;";
            public string countSql = "select count(*) from ActivityOutline where getdate() between startDate and endDate";
        }

        private class QueryEndSql
        {
            public string offsetSql = "select * from ActivityOutline where convert(varchar(10), getdate(), 23) > endDate order by endDate desc offset @Page_row rows fetch next 4 rows only;";
            public string countSql = "select count(*) from ActivityOutline where getdate() > endDate";

        }
        public ActionResult Index(string tag, int page = 1)
        {
            switch (tag)
            {
                case "即將開始":
                    SoonSql(page);
                    break;
                case "執行中":
                    NowSql(page);
                    break;
                case "已結束":
                    EndSql(page);
                    break;
                default:
                    AllSql(page);
                    break;
            }

            Tuple<List<classActivityOutline>, string, int, int> data = new Tuple<List<classActivityOutline>, string, int, int>(list, tag, maxPage, page);

            return View(data);
        }
        private void AllSql(int page)
        {
            QueryAllSql x = new QueryAllSql();
            list = OutlinetSql(x.offsetSql, page);
            maxPage = MaxPageSql(x.countSql);
        }

        private void SoonSql(int page)
        {
            QuerySoonSql x = new QuerySoonSql();
            list = OutlinetSql(x.offsetSql, page);
            maxPage = MaxPageSql(x.countSql);
        }

        private void NowSql(int page)
        {
            QueryNowSql x = new QueryNowSql();
            list = OutlinetSql(x.offsetSql, page);
            maxPage = MaxPageSql(x.countSql);
        }

        private void EndSql(int page)
        {
            QueryEndSql x = new QueryEndSql();
            list = OutlinetSql(x.offsetSql, page);
            maxPage = MaxPageSql(x.countSql);
        }

        private List<classActivityOutline> OutlinetSql(string strSql, int page)
        {
            SqlCommand cmd = OpenDatabase(strSql);
            cmd.Parameters.AddWithValue("@Page_row", (page - 1) * 4);
            SqlDataReader reader = cmd.ExecuteReader();
            List<classActivityOutline> list = ListSql(reader);

            reader.Close();
            con.Close();

            return list;
        }

        private List<classActivityOutline> ListSql(SqlDataReader reader)
        {
            List<classActivityOutline> list = new List<classActivityOutline>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    classActivityOutline x = new classActivityOutline()
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

        private int MaxPageSql(string strSql)
        {
            int count = 1;
            SqlCommand cmd = OpenDatabase(strSql);
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

        int rowNumber;
        int computeRowNumber;
        int previousRowsFetch = 3;
        int nextRowsFetch = 3;

        public ActionResult ActivityContent(int Id)
        {
            rowNumber = RowsNumberSql(Id);

            switch (rowNumber)
            {
                case 3:
                    computeRowNumber = 0;
                    previousRowsFetch = 2;
                    break;
                case 2:
                    computeRowNumber = 0;
                    previousRowsFetch = 1;
                    break;
                default:
                    computeRowNumber = rowNumber - 4;
                    break;
            }
            Tuple<classActivityContent, List<classActivityOutline>, List<classActivityOutline>> data = new Tuple<classActivityContent, List<classActivityOutline>, List<classActivityOutline>>(QueryContentById(Id), previousData(), nextData());

            return View(data);
        }

        private classActivityContent QueryContentById(int Id)
        {
            string IdSql = "select ActivityId, title, content from ActivityContent where ActivityId = @Id";

            SqlCommand cmd = OpenDatabase(IdSql);
            cmd.Parameters.AddWithValue("@Id", Id);
            classActivityContent x = ContentSql(cmd.ExecuteReader());

            return x;
        }

        private classActivityContent ContentSql(SqlDataReader reader)
        {
            classActivityContent x = new classActivityContent();
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

        private List<classActivityOutline> previousData()
        {
            List<classActivityOutline> previouslist = new List<classActivityOutline>();

            if (rowNumber != 1)
            {
                previouslist = OtherActivitySql(computeRowNumber, previousRowsFetch);
            }

            return previouslist;
        }
        private List<classActivityOutline> nextData()
        {
            return OtherActivitySql(rowNumber, nextRowsFetch);
        }
        private int RowsNumberSql(int Id)
        {
            string rowNumberSql = "with ActivityOutline as (select ROW_NUMBER() over (order by endDate desc) as ROW_ID, * from dbo.ActivityOutline) select * from ActivityOutline where Id = @Id";
            SqlCommand cmd = OpenDatabase(rowNumberSql);
            cmd.Parameters.AddWithValue("@Id", Id);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private List<classActivityOutline> OtherActivitySql(int computeRowNumber, int rowsFetch)
        {
            string offsetSql = "select * from ActivityOutline order by endDate desc, Id offset @computeRowNumber rows fetch next @rowsFetch rows only;";

            SqlCommand cmd = OpenDatabase(offsetSql);
            cmd.Parameters.AddWithValue("@computeRowNumber", computeRowNumber);
            cmd.Parameters.AddWithValue("@rowsFetch", rowsFetch);
            SqlDataReader reader = cmd.ExecuteReader();

            List<classActivityOutline> list = ListSql(reader);

            reader.Close();
            con.Close();

            return list;
        }
        SqlConnection a = new SqlConnection();

        private SqlCommand OpenDatabase(string strSQL)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["mssqlConnect"].ConnectionString);
            con.Open();
            return new SqlCommand(strSQL, con);
        }
    }
}