using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace prj_chuju.Models
{
    public class class_city
    {
        public int id;
        public string cityName;
        public class_city(SqlDataReader reader)
        {
            id = Convert.ToInt32(reader["id"]);
            cityName = reader["cityName"].ToString();
        }
    }
    public class class_region
    {
        public int id;
        public string regionName;
        public int cityID;
        public class_region(SqlDataReader reader)
        {
            id = Convert.ToInt32(reader["id"]);
            regionName = reader["regionName"].ToString();
            cityID = Convert.ToInt32(reader["cityID"]);
        }
    }
    public class class_allRegionInfo
    {
        public List<class_city> cityList;
        public List<class_region> regionList;
        public class_allRegionInfo()
        {
            factory_region f = new factory_region();
            cityList = f.getCityList();
            regionList = f.getRegionList();
        }
    }
    public class factory_region
    {
        private const string dbConnectioniStr = @"" +
            @"data source=chujudbserver.database.windows.net;" +
            @"initial catalog=dbchuju;" +
            @"user id=chujuas;" +
            @"password=P@ssw0rd-chuju;" +
            @"MultipleActiveResultSets=True;" +
            @"Asynchronous Processing=True;" +
            @"";
        public List<class_region> getRegionList()
        {
            List<class_region> output = new List<class_region>();
            string sqlstr = "select * from regionList";

            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd;
            SqlDataReader reader;

            con.Open();
            cmd = new SqlCommand(sqlstr, con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                output.Add(new class_region(reader));
            }
            con.Close();
            return output;
        }
        public List<class_city> getCityList()
        {
            List<class_city> output = new List<class_city>();
            string sqlstr = "select * from cityList";

            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd;
            SqlDataReader reader;

            con.Open();
            cmd = new SqlCommand(sqlstr, con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                output.Add(new class_city(reader));
            }
            con.Close();
            return output;
        }
    }
}