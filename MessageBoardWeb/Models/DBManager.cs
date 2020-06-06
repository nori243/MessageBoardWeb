using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MessageBoardWeb.Models
{
    public class DBManager
    {
        string connectString = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        SqlConnection connection;

        public DBManager()
        {
            connection = new SqlConnection(connectString);        
        }

        //-----user-----
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

        //-----message-----
        public List<Message> GetAllMessage()
        {
            connection.Open();
            List<Message> allMessage = new List<Message>();
            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = @"SELECT MessageId, MessageTitle,MessageContent,UserId,Time FROM [dbo].[Message]";

            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    int messageId = int.Parse(dataReader["MessageId"].ToString());
                    string messageTitle = dataReader["MessageTitle"].ToString();
                    string messageContent = dataReader["MessageContent"].ToString();
                    int userId = int.Parse(dataReader["UserId"].ToString());
                    DateTime time = (DateTime)dataReader["Time"];
                    Message message = new Message(messageId, userId, messageTitle, messageContent, time);
                    allMessage.Add(message);
                }
            }
            connection.Close();

            return allMessage;
        }

        public List<Message> GetMessageByText(string text)
        {
            List<Message> allMessage = new List<Message>();
            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = @"SELECT * FROM Message WHERE MessageContent LIKE @text OR MessageTitle LIKE @text";
            command.Parameters.AddWithValue("@text", "%"+text+"%");
            connection.Open();

            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    int messageId = int.Parse(dataReader["MessageId"].ToString());
                    string messageTitle = dataReader["MessageTitle"].ToString();
                    string messageContent = dataReader["MessageContent"].ToString();
                    int userId = int.Parse(dataReader["UserId"].ToString());
                    DateTime time = (DateTime)dataReader["Time"];
                    Message message = new Message(messageId, userId, messageTitle, messageContent, time);
                    allMessage.Add(message);
                }
            }
            connection.Close();

            return allMessage;
        }

        public List<Message> GetMessageByUserId(int userId)
        {
            connection.Open();
            List<Message> allMessage = new List<Message>();
            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = @"SELECT * FROM Message WHERE UserId = @id";
            command.Parameters.AddWithValue("@id", userId);

            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    int messageId = int.Parse(dataReader["MessageId"].ToString());
                    string messageTitle = dataReader["MessageTitle"].ToString();
                    string messageContent = dataReader["MessageContent"].ToString();
                    DateTime time = (DateTime)dataReader["Time"];
                    Message message = new Message(messageId, userId, messageTitle, messageContent, time);
                    allMessage.Add(message);
                }
            }
            connection.Close();

            return allMessage;
        }

        public void InsertMessage(Message message)
        {
            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = @"INSERT INTO Message(MessageTitle,MessageContent,UserId) VALUES (@messageTitle,@messageContent,@userId)";

            command.Parameters.AddWithValue("@messageTitle",message.MessageTitle);
            command.Parameters.AddWithValue("@messageContent",message.MessageContent);
            command.Parameters.AddWithValue("@userId",message.UserId);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();            
        }

        public Message GetMessageById(int id)
        {
            Message result = null;
            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = @"SELECT * FROM Message WHERE MessageId = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                int messageId = int.Parse(dataReader["MessageId"].ToString());
                string messageTitle = dataReader["MessageTitle"].ToString();
                string messageContent = dataReader["MessageContent"].ToString();
                int userId = int.Parse(dataReader["UserId"].ToString());
                DateTime time = (DateTime)dataReader["Time"];
                result = new Message(messageId, userId, messageTitle, messageContent, time);
            }

            connection.Close();
            return result;
        }

        public void DeleteMessageById(int id)
        {
            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = @"DELETE FROM Message WHERE MessageId = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdataMessageById(int id,Message updateMessage)
        {
            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = @"UPDATE Message SET MessageTitle = @messageTitle, MessageContent = @messageContent, UserId = @userId, Time = @time WHERE MessageId = @id";
            command.Parameters.AddWithValue("@messageTitle", updateMessage.MessageTitle);
            command.Parameters.AddWithValue("@messageContent", updateMessage.MessageContent);
            command.Parameters.AddWithValue("@userId", updateMessage.UserId);
            command.Parameters.AddWithValue("@time", DateTime.Now);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}