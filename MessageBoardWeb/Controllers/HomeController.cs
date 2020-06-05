﻿using MessageBoardWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace MessageBoardWeb.Controllers
{
    public class HomeController : Controller
    {
        string connectString = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;

        //----controller
        public ActionResult Index()
        {
            DBManager dBManager = new DBManager();
            List<Message> messageList = dBManager.GetAllMessage();
            Dictionary<Message, User> messageWithUserList = new Dictionary<Message, User>();

            foreach (Message message in messageList)
            {
                messageWithUserList.Add(message,dBManager.GetUserById(message.UserId));
            }

            ViewBag.messageList = messageWithUserList;

            return View();
        }

        public ActionResult MessageAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MessageAdd(Message message,string userName)
        {            
            DBManager dBManager = new DBManager();

            User user = dBManager.GetUserByName(userName);
            if (user == null)
            {
                dBManager.InsertUser(new User(userName));
            }

            try
            {
                message.UserId = dBManager.GetUserByName(userName).UserId;
                dBManager.InsertMessage(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteMessageDialog()
        {
            return View();
        }
    }
}