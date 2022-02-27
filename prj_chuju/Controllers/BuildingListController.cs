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
    public class BuildingListController : Controller
    {
         SqlConnection con;


        public ActionResult Index(int page = 1)
        {
            List<class_buildingList> list = default;
            int max_page = default;            
            string allSql = $"select * from buildingdb order by 建案序號 offset {(page - 1) *12} rows fetch next 12 rows only;";
            string all_countSql = "select count(*) from buildingdb;";
            list = ListSql(allSql,page);
            max_page = CountSql(all_countSql); 

            Tuple<List<class_buildingList>, int, int> data = new Tuple<List<class_buildingList>, int, int>(list, max_page, page);

            return View(data);

        }
       
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

        public ActionResult BuildingContent(int id)
        {
            List<Class_BuildingContent> list = default;
            var content = QueryContentById(id);


            Tuple<Class_BuildingContent, List<Class_BuildingContent>> data = new Tuple<Class_BuildingContent, List<Class_BuildingContent>>(content,list);
            return View(data);
        }

        
        private Class_BuildingContent QueryContentById(int id)
        {
            Class_BuildingContent x = new Class_BuildingContent();
            string IdSql = $"select 建案序號,縣市,地區,建案名稱,文宣1,文宣2,文宣3,文宣4,文宣5,文宣6,文宣7,文宣8,文宣9,pic1,pic2,pic3,pic4,pic5,pic6,pic7,pic8,pic9,pic10 from buildingdetail where 建案序號 = @id";
            SqlCommand cmd = methodSQL(IdSql);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            
            if (reader.Read())
            {
                x.id = (int)reader["建案序號"];
                x.country = (string)reader["縣市"];
                x.district = (string)reader["地區"];
                x.buildname = (string)reader["建案名稱"];                
                x.buildingtext1 = (string)reader["文宣1"];
                x.buildingtext2 = (string)reader["文宣2"];
                x.buildingtext3 = (string)reader["文宣3"];
                x.buildingtext4 = (string)reader["文宣4"];
                x.buildingtext5 = (string)reader["文宣5"];
                x.buildingtext6 = (string)reader["文宣6"];
                x.buildingtext7 = (string)reader["文宣7"];
                x.buildingtext8 = (string)reader["文宣8"];
                x.buildingtext9 = (string)reader["文宣9"];
                x.buildingpic1 = (string)reader["pic1"];
                x.buildingpic2 = (string)reader["pic2"];
                x.buildingpic3 = (string)reader["pic3"];
                x.buildingpic4 = (string)reader["pic4"];
                x.buildingpic5 = (string)reader["pic5"];
                x.buildingpic6 = (string)reader["pic6"];
                x.buildingpic7 = (string)reader["pic7"];
                x.buildingpic8 = (string)reader["pic8"];
                x.buildingpic9 = (string)reader["pic9"];
                x.buildingpic10 = (string)reader["pic10"];
                };
            
            reader.Close();
            con.Close();

            return x;
        }

        

    }
}