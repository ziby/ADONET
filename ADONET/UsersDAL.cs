using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ADONET
{
    class UsersDAL // Users data abstract layer
    {

        private SqlConnection _sqlCn = null;

        public void OpenConnection(string connectionString)
        {
            _sqlCn = new SqlConnection();
            _sqlCn.ConnectionString = connectionString;
            _sqlCn.Open();
        }

        public void InsertUser(User user)
        {
            string sqlQueryText = String.Format("Insert Into Users" +
                "(Id, FirstName, LastName, Login, Password, LastEntry, TextMessage)" +
                "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", user.Id, user.FirstName,
                user.LastName, user.Login, user.Password, user.LastEntry, user.TextMessage);

            using (var cmd = new SqlCommand(sqlQueryText, _sqlCn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateLastEntryUser(int id, DateTime dateTime)
        {
            string sqlQueryText = String.Format("Update Users Set LastEntry = '{0}' Where Id = '{1}'", dateTime, id);
            using (var cmd = new SqlCommand(sqlQueryText, _sqlCn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public int Login(String login, String password)
        {
            string sqlQueryText = String.Format("Select Id From Users" +
                "Where Login = {0} And Password = {1}", login, password);

            int id = -1;
            using (var cmd = new SqlCommand(sqlQueryText, _sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) id = (int)dr["Id"];
                dr.Close();
            }

            if (id != -1) UpdateLastEntryUser(id, DateTime.Now);
            return id;
        }

        
        public List<User> TopTen(bool isDeskLastEntry)
        {
            string sqlQueryText = String.Format("Select TOP(10) From Users Where Id Is Not Null OrderBy LastEntry"
                + (isDeskLastEntry ? "Desc" : ""));
            var topTen = new List<User>();
            using (var cmd = new SqlCommand(sqlQueryText, _sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    topTen.Add(new User()
                    {
                        Id = (int)dr["Id"], FirstName = (string)dr["FirstName"], LastName = (string)dr["LastName"], Password = null,
                        Login = (string)dr["Login"], LastEntry = (DateTime)dr["LastEntry"], TextMessage = (string)dr["TextMessage"]
                    });
                }
            }
            return topTen;
        }

        public void UpdateTextMessage(int id, string newTextMessage)
        {
            string sqlQueryText = String.Format("Update Users Set TextMessage = '{0}' Where Id = '{1}'", newTextMessage, id);
            using (var cmd = new SqlCommand(sqlQueryText, _sqlCn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void CloseConnection()
        {
            _sqlCn.Close();
        }
    }
}
