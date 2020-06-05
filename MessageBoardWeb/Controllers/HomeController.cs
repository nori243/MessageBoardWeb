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
            DBManager dBManager = new DBManager();
            ViewBag.messageList = dBManager.GetAllMessage();
            return View();
        }

        public ActionResult MessageAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MessageAdd(Message message)
        {            
            DBManager dBManager = new DBManager();
            try
            {
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