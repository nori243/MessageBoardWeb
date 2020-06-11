using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MessageBoardWeb.Models
{
    public class UserDBManager
    {
        string connectString = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        SqlConnection connection;

        public UserDBManager()
        {
            connection = new SqlConnection(connectString);
        }

        public List<User> GetAllUser()
        {
            connection.Open();
            List<User> allUser = new List<User>();
            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = @"SELECT UserId, UserName FROM [dbo].[UserInfo]";

            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    int userId = int.Parse(dataReader["UserId"].ToString());
                    string userName = dataReader["UserName"].ToString();
                    User user = new User(userId, userName);
                    allUser.Add(user);
                }
            }
            connection.Close();

            return allUser;
        }

        public void InsertUser(User user)
        {
            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = @"INSERT INTO UserInfo(UserName) VALUES (@userName)";

            command.Parameters.AddWithValue("@userName", user.UserName);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public User GetUserById(int id)
        {
            User result = null;
            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = @"SELECT * FROM UserInfo WHERE UserId = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                int userId = int.Parse(dataReader["UserId"].ToString());
                string userName = dataReader["UserName"].ToString();
                result = new User(userId, userName);
            }

            connection.Close();
            return result;
        }

        public User GetUserByName(string name)
        {
            User result = null;
            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = @"SELECT * FROM UserInfo WHERE UserName = @name";
            command.Parameters.AddWithValue("@name", name);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                int userId = int.Parse(dataReader["UserId"].ToString());
                string userName = dataReader["UserName"].ToString();
                result = new User(userId, userName);
            }

            connection.Close();
            return result;
        }

        public void DeleteUserById(int id)
        {
            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = @"DELETE FROM UserInfo WHERE UserId = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateUserById(int id, User updateUser)
        {
            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = @"UPDATE UserInfo SET UserName = @userName WHERE UserId = @id";
            command.Parameters.AddWithValue("@userName", updateUser.UserName);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}