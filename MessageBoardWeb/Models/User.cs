using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoardWeb.Models
{
    public class User
    {
        public string UserName { get; set; }
        public int UserId { get; set; }

        public User(int id, string name)
        {
            UserId = id;
            UserName = name;
        }
    }
}