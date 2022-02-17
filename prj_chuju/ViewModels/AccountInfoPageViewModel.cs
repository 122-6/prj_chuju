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
        public class_regionInfo regionInfo;

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
                regionInfo = new class_regionInfo(reader);
            }
            else
            {
                accountInfo = new class_accountInfo();
                regionInfo = new class_regionInfo();
            }
            con.Close();
        }

    }
    public class class_regionInfo
    {
        public int regionID;
        public int cityID;
        public string regionName;
        public string cityName;

        public class_regionInfo()
        {
            regionID = 1;
            cityID = 1;
            regionName = "中山區";
            cityName = "臺北市";
        }
        public class_regionInfo(SqlDataReader reader)
        {
            regionID = reader["region"].Equals(DBNull.Value) ? -1 : Convert.ToInt32(reader["region"]);
            cityID = reader["cityID"].Equals(DBNull.Value) ? -1 : Convert.ToInt32(reader["cityID"]);
            regionName = reader["regionName"].ToString();
            cityName = reader["cityName"].ToString();
        }

    }
}