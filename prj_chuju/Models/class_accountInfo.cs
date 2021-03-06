using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// sql相關
using System.Data.SqlClient;

namespace prj_chuju.Models
{
    public class class_accountInfo
    {
        public int id;
        public string permission;
        public string userPassword;
        public string email;
        public string userName;
        public DateTime birthday;
        public string gender;
        public string cellphone;
        public int region;
        public int avatar;
        public string avatarURL;

        public class_accountInfo()
        {
            id = -1;
            permission = "client";
            userPassword = "initPassword";
            email = "initEmail";
            userName = "initUserName";
            birthday = new DateTime(1900, 1, 1);
            gender = "initGender";
            cellphone = "initPhone";
            region = -1;
            avatar = -1;
            avatarURL = "";
        }
        public class_accountInfo(SqlDataReader reader)
        {
            id = reader["id"].Equals(DBNull.Value) ? -1 : Convert.ToInt32(reader["id"]);
            birthday = reader["birthday"].Equals(DBNull.Value) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["birthday"]);
            region = reader["region"].Equals(DBNull.Value) ? -1 : Convert.ToInt32(reader["region"]);
            avatar = reader["avatar"].Equals(DBNull.Value) ? -1 : Convert.ToInt32(reader["avatar"]);

            permission = reader["permission"].ToString();
            userPassword = reader["userPassword"].ToString();
            email = reader["email"].ToString();
            userName = reader["userName"].ToString();
            gender = reader["gender"].ToString();
            cellphone = reader["cellphone"].ToString();
            avatarURL = reader["avatarURL"].ToString();
        }
    }
    public class factory_accountInfo
    {
        private const string dbConnectioniStr = @"" +
            @"data source=chujudbserver.database.windows.net;" +
            @"initial catalog=dbchuju;" +
            @"user id=chujuas;" +
            @"password=P@ssw0rd-chuju;" +
            @"MultipleActiveResultSets=True;" +
            @"Asynchronous Processing=True;" +
            @"";

        // 新增會員
        public void create(string cellphone, string email)
        {
            string cmdstr = "insert into accountInfo (accountName,userPassword,userName,email,cellphone) " +
                "values (@accountNamePara,@userPasswordPara,@userNamePara,@emailPara,@cellphonePara)";

            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            cmd.Parameters.AddWithValue("@accountNamePara","account");
            cmd.Parameters.AddWithValue("@userPasswordPara","password");
            cmd.Parameters.AddWithValue("@userNamePara","username");
            cmd.Parameters.AddWithValue("@emailPara",email);
            cmd.Parameters.AddWithValue("@cellphonePara",cellphone);

            con.Open();
            int row = cmd.ExecuteNonQuery();
            con.Close();
        }
        public void create(HttpRequestBase req)
        {
            string cmdstr = "insert into accountInfo (permission,userPassword,userName,email,cellphone,birthday) " +
                "values (@permissionPara,@userPasswordPara,@userNamePara,@emailPara,@cellphonePara,@birthdyPara)";

            string password = req.Form["password"];
            string userName = req.Form["userName"];
            string userEmail = req.Form["userEmail"];
            string userPhone = req.Form["userPhone"];
            string birthDay = req.Form["birthDay"];

            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            cmd.Parameters.AddWithValue("@permissionPara", "client");
            cmd.Parameters.AddWithValue("@userPasswordPara", password);
            cmd.Parameters.AddWithValue("@userNamePara", userName);
            cmd.Parameters.AddWithValue("@emailPara", userEmail);
            cmd.Parameters.AddWithValue("@cellphonePara", userPhone);
            cmd.Parameters.AddWithValue("@birthdyPara", birthDay);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception ex)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@permissionPara", "client");
                cmd.Parameters.AddWithValue("@userPasswordPara", password);
                cmd.Parameters.AddWithValue("@userNamePara", userName);
                cmd.Parameters.AddWithValue("@emailPara", userEmail);
                cmd.Parameters.AddWithValue("@cellphonePara", userPhone);
                cmd.Parameters.AddWithValue("@birthdyPara", "1900-1-1");

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch
                {

                }
            }
            
        }

        // 調閱會員資料
        public class_accountInfo selectAccountByID(int theid)
        {
            string sqlstr = "select ai.*, ap.pictureURL as avatarURL from accountInfo as ai " +
                "left join accountPicture as ap on ap.id = ai.avatar " +
                "where ai.id = @idPara";
            class_accountInfo res=new class_accountInfo();

            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd;
            SqlDataReader reader;

            con.Open();
            cmd = new SqlCommand(sqlstr, con);
            cmd.Parameters.AddWithValue("@idPara", theid);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                res = new class_accountInfo(reader);
            }
            con.Close();
            return res;
        }
        public int getNewestAvatar(int userID)
        {
            int result = -1;
            string sqlstr = "select top(1) id from accountPicture where uploader=@userIDPara order by uploadDate desc";
            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            SqlDataReader reader;
            cmd.Parameters.AddWithValue("@userIDPara", userID);
            con.Open();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                result = Convert.ToInt32(reader["id"]);
            }
            con.Close();
            return result;
        }

        // 驗證系統
        public int occupiedEmailID(string enterEmail)
        {
            string cmdstr = "select id from accountInfo where email = @emailPara";

            int theid = -1;
            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            SqlDataReader reader;
            cmd.Parameters.AddWithValue("@emailPara", enterEmail);
            con.Open();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                theid = Convert.ToInt32(reader["id"]);
            }
            con.Close();
            return theid;
        }
        public int occupiedPhoneID(string enterPhone)
        {
            string cmdstr = "select id from accountInfo where cellphone = @cellphonePara";

            int theid = -1;
            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            SqlDataReader reader;
            cmd.Parameters.AddWithValue("@cellphonePara", enterPhone);
            con.Open();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                theid = Convert.ToInt32(reader["id"]);
            }
            con.Close();
            return theid;
        }
        public bool validateByEmail(string email,string passwordEntered)
        {
            bool AccountFound = false;
            string cmdstr = "select userPassword from accountInfo where email = @emailPara";
            string thePassword = "\0";

            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            SqlDataReader reader;
            cmd.Parameters.AddWithValue("@emailPara", email);
            con.Open();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                thePassword = reader["userPassword"].ToString();
                AccountFound = true;
            }
            con.Close();

            if (AccountFound)
            {
                return passwordEntered == thePassword;
            }
            return false;
        }
        public int varifyPassByID(int theid,string password)
        {
            if (password == "") return -1;

            int passUserID = -1;
            string cmdstr = "select id from accountInfo where id = @idPara and userPassword = @passwordPara";

            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            SqlDataReader reader;
            cmd.Parameters.AddWithValue("@idPara", theid);
            cmd.Parameters.AddWithValue("@passwordPara", password);

            con.Open();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                passUserID = Convert.ToInt32(reader["id"]);
            }
            con.Close();

            return passUserID;
        }

        // 修改會員系統
        public void editAccountInfo(HttpRequestBase request)
        {
            string sqlstr = "";
            if (request["region"] == "")
            {
                sqlstr = "update accountInfo set " +
                "userName=@userNamePara," +
                "gender=@genderPara," +
                "birthday=@birthdayPara," +
                "email=@emailPara," +
                "cellphone=@cellphonePara " +
                "where id = @idPara;";
            }
            else
            {
                sqlstr = "update accountInfo set " +
                "userName=@userNamePara," +
                "gender=@genderPara," +
                "birthday=@birthdayPara," +
                "email=@emailPara," +
                "region=@regionPara," +
                "cellphone=@cellphonePara " +
                "where id = @idPara;";
            }
            
            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd;
            cmd = new SqlCommand(sqlstr, con);
            cmd.Parameters.AddWithValue("@userNamePara", request["userName"]);
            cmd.Parameters.AddWithValue("@genderPara", request["gender"]);
            cmd.Parameters.AddWithValue("@birthdayPara", request["birthday"]);
            cmd.Parameters.AddWithValue("@emailPara", request["email"]);
            if (request["region"] != "")
                cmd.Parameters.AddWithValue("@regionPara", request["region"]);
            cmd.Parameters.AddWithValue("@cellphonePara", request["cellphone"]);
            cmd.Parameters.AddWithValue("@idPara", request["id"]);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {

            }
        }
        public void editAccountPassword(HttpRequestBase request)
        {
            string sqlstr = "update accountInfo set userPassword=@newPassword where id = @theid";
            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd;
            cmd = new SqlCommand(sqlstr, con);
            cmd.Parameters.AddWithValue("@newPassword", request["password"]);
            cmd.Parameters.AddWithValue("@theid", request["theid"]);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void createAvatar(string fileName, int userID)
        {
            string sqlstr = "insert into accountPicture (pictureURL,uploader) values (@theURL,@userIDPara)";
            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd;

            cmd = new SqlCommand(sqlstr, con);
            cmd.Parameters.AddWithValue("@theURL", fileName);
            cmd.Parameters.AddWithValue("@userIDPara", userID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void updateUserAvatar(int userID,int avatarID)
        {
            string sqlstr = "update accountInfo set avatar = @avatarIDPara where id = @userIDPara";
            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            cmd.Parameters.AddWithValue("@avatarIDPara", avatarID);
            cmd.Parameters.AddWithValue("@userIDPara", userID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        // 忘記密碼系統
        public void addForgetCode(int theid,string code)
        {
            string sqlstr = "insert into forgetCode (accountID,theCode) values (@idPara,@codePara)";
            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            cmd.Parameters.AddWithValue("@idPara", theid);
            cmd.Parameters.AddWithValue("@codePara", code);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public int getIDByCode(string code)
        {
            int output = -1;
            string sqlstr = "select accountID from forgetCode where theCode = @codePara";
            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            SqlDataReader reader;

            con.Open();
            cmd.Parameters.AddWithValue("@codePara", code);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                output = Convert.ToInt32(reader["accountID"]);
            }
            con.Close();
            return output;
        }

        public void removeCodeByID(int theid)
        {
            string sqlstr = "delete from forgetCode where accountID = @idPara";
            SqlConnection con = new SqlConnection(dbConnectioniStr);
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            cmd.Parameters.AddWithValue("@idPara", theid);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }

    
}