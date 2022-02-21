using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using prj_chuju.Models;

namespace prj_chuju.ViewModels
{
    public class AccountInfoPageViewModel
    {
        private const string dbConnectioniStr = @"" +
               @"data source=chujudbserver.database.windows.net;" +
               @"initial catalog=dbchuju;" +
               @"user id=chujuas;" +
               @"password=P@ssw0rd-chuju;" +
               @"MultipleActiveResultSets=True;" +
               @"Asynchronous Processing=True;" +
               @"";
        public class_accountInfo accountInfo;
        public class_userRegionInfo regionInfo;

        public AccountInfoPageViewModel(int theid)
        {
            string sqlstr = "select * from accountInfo as ai " +
                "left join regionList as rl on ai.region = rl.id " +
                "left join cityList as cl on cl.id = rl.cityID " +
                "where ai.id = @idPara";

            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd;
            SqlDataReader reader;

            con.Open();
            cmd = new SqlCommand(sqlstr, con);
            cmd.Parameters.AddWithValue("@idPara", theid);

            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                accountInfo = new class_accountInfo(reader);
                regionInfo = new class_userRegionInfo(reader);
            }
            else
            {
                accountInfo = new class_accountInfo();
                regionInfo = new class_userRegionInfo();
            }
            con.Close();
        }

    }
    public class class_userRegionInfo
    {
        public int regionID;
        public int cityID;
        public string regionName;
        public string cityName;

        public class_userRegionInfo()
        {
            regionID = 1;
            cityID = 1;
            regionName = "中山區";
            cityName = "臺北市";
        }
        public class_userRegionInfo(SqlDataReader reader)
        {
            regionID = reader["region"].Equals(DBNull.Value) ? -1 : Convert.ToInt32(reader["region"]);
            cityID = reader["cityID"].Equals(DBNull.Value) ? -1 : Convert.ToInt32(reader["cityID"]);
            regionName = reader["regionName"].ToString();
            cityName = reader["cityName"].ToString();
        }

    }
    public class class_userBuildingInfo
    {
        public int 建案序號;
        public string 建案名稱;
        public string 地區;
        public string 坪數規劃;
        public string picURL;
        public class_userBuildingInfo(SqlDataReader reader)
        {
            建案序號 = Convert.ToInt32(reader["建案序號"]);
            建案名稱 = reader["建案名稱"].ToString();
            地區 = reader["地區"].ToString();
            坪數規劃 = reader["坪數規劃"].ToString();
            picURL = reader["picURL"].ToString();
        }
    }
    public class class_userArticleInfo
    {
        public int id;
        public string mainClass;
        public string className;
        public string title;
        public string publishDate;
        public string picURL;
        public class_userArticleInfo(SqlDataReader reader)
        {
            id = Convert.ToInt32(reader["id"]);
            mainClass = reader["mainClass"].ToString() == "買房第一站" ? "BuyHouseFirst" : "MarketNews";
            className = reader["className"].ToString();
            title = reader["title"].ToString();
            publishDate = reader["publishDate"].ToString();
            picURL = reader["picURL"].ToString();
        }
    }
    public class factory_userCollectViewed
    {
        private const string dbConnectioniStr = @"" +
            @"data source=chujudbserver.database.windows.net;" +
            @"initial catalog=dbchuju;" +
            @"user id=chujuas;" +
            @"password=P@ssw0rd-chuju;" +
            @"MultipleActiveResultSets=True;" +
            @"Asynchronous Processing=True;" +
            @"";

        public List<class_userBuildingInfo> getCollectBuilding(int theid)
        {
            List<class_userBuildingInfo> output = new List<class_userBuildingInfo>();
            string sqlstr = "select 建案序號, 建案名稱, 地區, 坪數規劃, picURL " +
                "from collectBuilding as cb " +
                "join buildingdb as bdb on cb.buildingID=bdb.建案序號 " +
                "where accountID = @theid order by collectDate desc";

            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd;
            SqlDataReader reader;

            con.Open();
            cmd = new SqlCommand(sqlstr, con);
            cmd.Parameters.AddWithValue("@theid", theid);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                output.Add(new class_userBuildingInfo(reader));
            }
            con.Close();
            return output;
        }
        public List<class_userBuildingInfo> getViewedBuilding(int theid)
        {
            List<class_userBuildingInfo> output = new List<class_userBuildingInfo>();
            string sqlstr = "select 建案序號, 建案名稱, 地區, 坪數規劃, picURL " +
                "from viewedBuilding as vb " +
                "join buildingdb as bdb on vb.buildingID=bdb.建案序號 " +
                "where accountID = @theid order by viewDate desc";

            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd;
            SqlDataReader reader;

            con.Open();
            cmd = new SqlCommand(sqlstr, con);
            cmd.Parameters.AddWithValue("@theid", theid);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                output.Add(new class_userBuildingInfo(reader));
            }
            con.Close();
            return output;
        }

        public List<class_userArticleInfo> getCollectArticle(int theid)
        {
            List<class_userArticleInfo> output = new List<class_userArticleInfo>();
            string sqlstr = "select ca.articleID as id, className, title, publishDate, pictureURL as picURL, mainClass from collectArticle as ca " +
                "join articleOutline as ao on ao.id = ca.articleID " +
                "join articleClass as ac on ac.id = ao.articleClass " +
                "where accountID = @theid order by collectDate desc";
            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd;
            SqlDataReader reader;

            con.Open();
            cmd = new SqlCommand(sqlstr, con);
            cmd.Parameters.AddWithValue("@theid", theid);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                output.Add(new class_userArticleInfo(reader));
            }
            con.Close();
            return output;
        }
        public List<class_userArticleInfo> getViewedArticle(int theid)
        {
            List<class_userArticleInfo> output = new List<class_userArticleInfo>();
            string sqlstr = "select va.articleID as id, className, title, publishDate, pictureURL as picURL, mainClass from viewedArticle as va " +
                "join articleOutline as ao on ao.id = va.articleID " +
                "join articleClass as ac on ac.id = ao.articleClass " +
                "where accountID = @theid order by viewDate desc";
            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd;
            SqlDataReader reader;

            con.Open();
            cmd = new SqlCommand(sqlstr, con);
            cmd.Parameters.AddWithValue("@theid", theid);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                output.Add(new class_userArticleInfo(reader));
            }
            con.Close();
            return output;
        }
    }
}