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
         SqlConnection con;

        //static SqlConnectionStringBuilder myBuilder = new SqlConnectionStringBuilder()
        //{
        //    DataSource = "chujudbserver.database.windows.net",
        //    InitialCatalog = "dbchuju",
        //    UserID = "chujuas",
        //    Password = "P@ssw0rd-chuju",
        //    AsynchronousProcessing = true,
        //};
        //static string conStr = myBuilder.ToString();


        // GET: BuildingList


        public ActionResult Index(string tag,int page = 1)
        {
            List<class_buildingList> list = default;
            int max_page = default;

            string allSql = $"select * from buildingdb order by 建案序號 offset {(page - 1) *12} rows fetch next 12 rows only;";
            string all_countSql = "select count(*) from buildingdb;";
            list = ListSql(allSql,page);
            max_page = CountSql(all_countSql); 

            Tuple<List<class_buildingList>,string, int, int> data = new Tuple<List<class_buildingList>,string, int, int>(list,tag, max_page, page);

            return View(data);

        }
        //List<class_buildingList> list = new List<class_buildingList>();

        //SqlConnection con = new SqlConnection(conStr);
        //SqlCommand cmd;
        //SqlDataReader reader;

        //con.Open();
        //cmd = new SqlCommand("select * from buildingdb", con);
        //reader = cmd.ExecuteReader();
        //while (reader.Read())
        //{
        //    class_buildingList bl  = new class_buildingList(reader);
        //    list.Add(bl);
        //}
        //con.Close();

        //return View(list);

        private List<class_buildingList> ListSql(string strSql,int page)
        {
            SqlCommand cmd = methodSQL(strSql);
            cmd.Parameters.AddWithValue("@Page_row", (page - 1) * 12);
            SqlDataReader reader = cmd.ExecuteReader();
            List<class_buildingList> list = new List<class_buildingList>();
            while (reader.Read())
            {
                class_buildingList x = new class_buildingList()
                {
                id = Convert.ToInt32(reader["建案序號"]),
                country = reader["縣市"].ToString(),
                district = reader["地區"].ToString(),
                buildname = reader["建案名稱"].ToString(),
                insidespace = reader["坪數規劃"].ToString(),
                floor = reader["樓層規劃"].ToString(),
                space = Convert.ToInt32(reader["基地面積"]),
                location = reader["基地位置"].ToString(),
                reception = reader["接待中心"].ToString(),
                phone = reader["諮詢專線"].ToString(),
                features = reader["特點"].ToString(),
                situation = reader["現況"].ToString(),
                pictrueurl = reader["picURL"].ToString()
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
                count = (int)reader[0] / 12;
                if ((int)reader[0] % 12 != 0)
                {
                    count++;
                }
            }
            reader.Close();
            con.Close();

            return (count);
        }

        private SqlCommand methodSQL(string strSQL)
        {
            con = new SqlConnection();
            con.ConnectionString = @"Data Source=chujudbserver.database.windows.net;Initial Catalog=dbchuju;Persist Security Info=True;User ID=chujuas;Password=P@ssw0rd-chuju;MultipleActiveResultSets=True;Application Name=EntityFramework";
            con.Open();
            return new SqlCommand(strSQL, con);
        }

        public ActionResult BuildingContent(int Id)
        {
            var content = QueryContentById(Id);
            return View(content);
        }

        private Class_BuildingContent QueryContentById(int Id)
        {
            Class_BuildingContent x = default;
            string IdSql = $"select 縣市,地區,建案名稱,文宣1,文宣2,文宣3,文宣4,文宣5,文宣6,文宣7,文宣8,文宣9,pic1,pic2,pic3,pic4,pic5,pic6,pic7,pic8,pic9 from buildingdetail where  建案序號 = @Id";

            SqlCommand cmd = methodSQL(IdSql);
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                x = new Class_BuildingContent()
                {
                    id = (int)reader["建案序號"],
                    country = (string)reader["縣市"],
                    district = (string)reader["區域"],
                    buildingname = (string)reader["建案名稱"],
                    buildingtext1 = (string)reader["文宣1"],
                    buildingtext2 = (string)reader["文宣2"],
                    buildingtext3 = (string)reader["文宣3"],
                    buildingtext4 = (string)reader["文宣4"],
                    buildingtext5 = (string)reader["文宣5"],
                    buildingtext6 = (string)reader["文宣6"],
                    buildingtext7 = (string)reader["文宣7"],
                    buildingtext8 = (string)reader["文宣8"],
                    buildingtext9 = (string)reader["文宣9"],
                    buildingpic1 = (string)reader["pic1"],
                    buildingpic2 = (string)reader["pic2"],
                    buildingpic3 = (string)reader["pic3"],
                    buildingpic4 = (string)reader["pic4"],
                    buildingpic5 = (string)reader["pic5"],
                    buildingpic6 = (string)reader["pic6"],
                    buildingpic7 = (string)reader["pic7"],
                    buildingpic8 = (string)reader["pic8"],
                    buildingpic9 = (string)reader["pic9"],

                };
            }
            reader.Close();
            con.Close();

            return x;
        }

    }
}