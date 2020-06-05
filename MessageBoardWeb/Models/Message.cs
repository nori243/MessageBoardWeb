using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoardWeb.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string MessageTitle { get; set; }
        public string MessageContent { get; set; }
        public int UserId { get; set; }
        public DateTime Time { get; set; }

        public Message(int userId, string title,string content)
        {
            UserId = userId;
            MessageTitle = title;
            MessageContent = content;
            Time = DateTime.Now;
        }

        public Message(int id, int userId, string title, string content, DateTime dateTime)
        {
            MessageId = id;
            UserId = userId;
            MessageTitle = title;
            MessageContent = content;
            Time = dateTime;
        }
    }
}