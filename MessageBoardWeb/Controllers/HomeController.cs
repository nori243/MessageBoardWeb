using MessageBoardWeb.Models;
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
            MessageDBManager messageDBManager = new MessageDBManager();
            List<Message> messageList = messageDBManager.GetAllMessage();
            Dictionary<Message, User> messageWithUserList = new Dictionary<Message, User>();

            if (TempData["messageList"] == null)
            {
                UserDBManager userDBManager = new UserDBManager();
                foreach (Message message in messageList)
                {
                    messageWithUserList.Add(message, userDBManager.GetUserById(message.UserId));
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
            UserDBManager userDBManager = new UserDBManager();

            User user = userDBManager.GetUserByName(userName);
            if (user == null)
            {
                userDBManager.InsertUser(new User(userName));
            }

            MessageDBManager messageDBManager = new MessageDBManager();
            try
            {
                message.UserId = userDBManager.GetUserByName(userName).UserId;
                messageDBManager.InsertMessage(message);
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
            MessageDBManager messageDBManager = new MessageDBManager();
            messageDBManager.DeleteMessageById(id);
            return RedirectToAction("Index");
        }

        public ActionResult EditView(int id)
        {
            MessageDBManager messageDBManager = new MessageDBManager();
            UserDBManager userDBManager = new UserDBManager();

            Message orgMessage = messageDBManager.GetMessageById(id);
            User user = userDBManager.GetUserById(orgMessage.UserId);
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
            MessageDBManager messageDBManager = new MessageDBManager();
            UserDBManager userDBManager = new UserDBManager();
            ViewBag.orgMessage = (Message)TempData["orgMessage"];
            ViewBag.orgUser = (User)TempData["orgUser"];
            User user = userDBManager.GetUserByName(userName);
            if (user == null)
            {
                userDBManager.InsertUser(new User(userName));
            }

            try
            {
                message.UserId = userDBManager.GetUserByName(userName).UserId;
                messageDBManager.UpdataMessageById(message.MessageId,message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return RedirectToAction("Index");
        }

        public ActionResult SearchByUser(string userName)
        {
            MessageDBManager messageDBManager = new MessageDBManager();
            UserDBManager userDBManager = new UserDBManager();
            User user = userDBManager.GetUserByName(userName);
            List<Message> messageList = messageDBManager.GetMessageByUserId(user.UserId);
            Dictionary<Message, User> messageWithUserList = new Dictionary<Message, User>();

            foreach (Message message in messageList)
            {
                messageWithUserList.Add(message, userDBManager.GetUserById(message.UserId));
            }

            TempData["messageList"] = messageWithUserList;
            return RedirectToAction("Index");
        }

        public ActionResult SearchByText(string text)
        {
            MessageDBManager messageDBManager = new MessageDBManager();
            UserDBManager userDBManager = new UserDBManager();
            List<Message> messageList = messageDBManager.GetMessageByText(text);
            Dictionary<Message, User> messageWithUserList = new Dictionary<Message, User>();

            foreach (Message message in messageList)
            {
                messageWithUserList.Add(message, userDBManager.GetUserById(message.UserId));
            }

            TempData["messageList"] = messageWithUserList;
            return RedirectToAction("Index");
        }
    }
}