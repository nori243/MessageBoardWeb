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
            DBManager dBManager = DBManager.GetInstanceDBManager();
            List<Message> messageList = dBManager.GetAllMessage();
            Dictionary<Message, User> messageWithUserList = new Dictionary<Message, User>();

            if (TempData["messageList"] == null)
            {
                foreach (Message message in messageList)
                {
                    messageWithUserList.Add(message, dBManager.GetUserById(message.UserId));
                }

                TempData["messageList"] = messageWithUserList;
            }
           return View();
        }

        public ActionResult MessageAdd()
        {
            return View();
        }


        [HttpPost]
        public ActionResult MessageAdd(Message message,string userName)
        {            
            DBManager dBManager = DBManager.GetInstanceDBManager();

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

        public ActionResult DeleteMessageDialog(int id)
        {
            int messageId = id;
            DBManager dBManager = DBManager.GetInstanceDBManager();
            dBManager.DeleteMessageById(id);
            return RedirectToAction("Index");
        }

        public ActionResult EditView(int id)
        {
            DBManager dBManager = DBManager.GetInstanceDBManager();
            Message orgMessage = dBManager.GetMessageById(id);
            User user = dBManager.GetUserById(orgMessage.UserId);
            TempData["orgMessage"] = orgMessage;
            TempData["orgUser"] = user;
            return RedirectToAction("EditMessageView");
        }

        public ActionResult EditMessageView()
        {
            ViewBag.orgMessage = (Message)TempData["orgMessage"];
            ViewBag.orgUser = (User)TempData["orgUser"];
            return View();
        }

        [HttpPost]
        public ActionResult EditMessageView(Message message,string userName)
        {
            DBManager dBManager = DBManager.GetInstanceDBManager();
            ViewBag.orgMessage = (Message)TempData["orgMessage"];
            ViewBag.orgUser = (User)TempData["orgUser"];
            User user = dBManager.GetUserByName(userName);
            if (user == null)
            {
                dBManager.InsertUser(new User(userName));
            }

            try
            {
                message.UserId = dBManager.GetUserByName(userName).UserId;
                dBManager.UpdataMessageById(message.MessageId,message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return RedirectToAction("Index");
        }

        public ActionResult SearchByUser(string userName)
        {
            DBManager dBManager = DBManager.GetInstanceDBManager();
            User user = dBManager.GetUserByName(userName);
            List<Message> messageList = dBManager.GetMessageByUserId(user.UserId);
            Dictionary<Message, User> messageWithUserList = new Dictionary<Message, User>();

            foreach (Message message in messageList)
            {
                messageWithUserList.Add(message, dBManager.GetUserById(message.UserId));
            }

            TempData["messageList"] = messageWithUserList;
            return RedirectToAction("Index");
        }

        public ActionResult SearchByText(string text)
        {
            DBManager dBManager = DBManager.GetInstanceDBManager();
            List<Message> messageList = dBManager.GetMessageByText(text);
            Dictionary<Message, User> messageWithUserList = new Dictionary<Message, User>();

            foreach (Message message in messageList)
            {
                messageWithUserList.Add(message, dBManager.GetUserById(message.UserId));
            }

            TempData["messageList"] = messageWithUserList;
            return RedirectToAction("Index");
        }
    }
}