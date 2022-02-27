using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prj_chuju.Models
{
    public class class_CollectAndViewed
    {
    }
    public class factory_CollectAndViewed
    {
        private const string dbConnectioniStr = @"" +
            @"data source=chujudbserver.database.windows.net;" +
            @"initial catalog=dbchuju;" +
            @"user id=chujuas;" +
            @"password=P@ssw0rd-chuju;" +
            @"MultipleActiveResultSets=True;" +
            @"Asynchronous Processing=True;" +
            @"";
        public void visitBuilding(int userID, int buildingID)
        {
            if (userID > 0 && buildingID > 0)
            {
                string sqlstrDEL = "delete from viewedBuilding where accountID=@userIDPara and buildingID=@buildingIDPara";
                string sqlstrINS = "insert into viewedBuilding (accountID,buildingID) values (@userIDPara,@buildingIDPara)";
                SqlConnection con = new SqlConnection(dbConnectioniStr);
                SqlCommand cmd;
                con.Open();

                cmd = new SqlCommand(sqlstrDEL, con);
                cmd.Parameters.AddWithValue("@userIDPara", userID);
                cmd.Parameters.AddWithValue("@buildingIDPara", buildingID);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand(sqlstrINS, con);
                cmd.Parameters.AddWithValue("@userIDPara", userID);
                cmd.Parameters.AddWithValue("@buildingIDPara", buildingID);
                cmd.ExecuteNonQuery();

                con.Close();
            }
        }
        public void collectBuilding(int userID, int buildingID)
        {
            if (userID > 0 && buildingID > 0)
            {
                string sqlstrDEL = "delete from collectBuilding where accountID=@userIDPara and buildingID=@buildingIDPara";
                string sqlstrINS = "insert into collectBuilding (accountID,buildingID) values (@userIDPara,@buildingIDPara)";
                SqlConnection con = new SqlConnection(dbConnectioniStr);
                SqlCommand cmd;
                con.Open();

                cmd = new SqlCommand(sqlstrDEL, con);
                cmd.Parameters.AddWithValue("@userIDPara", userID);
                cmd.Parameters.AddWithValue("@buildingIDPara", buildingID);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand(sqlstrINS, con);
                cmd.Parameters.AddWithValue("@userIDPara", userID);
                cmd.Parameters.AddWithValue("@buildingIDPara", buildingID);
                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void visitArticle(int userID, int articleID)
        {
            if (userID > 0 && articleID > 0)
            {
                string sqlstrDEL = "delete from viewedArticle where accountID=@userIDPara and articleID=@articleIDPara";
                string sqlstrINS = "insert into viewedArticle (accountID,articleID) values (@userIDPara,@articleIDPara)";
                SqlConnection con = new SqlConnection(dbConnectioniStr);
                SqlCommand cmd;
                con.Open();

                cmd = new SqlCommand(sqlstrDEL, con);
                cmd.Parameters.AddWithValue("@userIDPara", userID);
                cmd.Parameters.AddWithValue("@articleIDPara", articleID);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand(sqlstrINS, con);
                cmd.Parameters.AddWithValue("@userIDPara", userID);
                cmd.Parameters.AddWithValue("@articleIDPara", articleID);
                cmd.ExecuteNonQuery();

                con.Close();
            }
        }
    }
}