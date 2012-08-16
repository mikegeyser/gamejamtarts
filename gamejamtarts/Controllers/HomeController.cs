﻿using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using gamejamtarts.Models;

namespace gamejamtarts.Controllers
{
    public class HomeController : Controller
    {
        private Db db = new Db();

        public ActionResult Index()
        {
            return View(db.Games.Take(2).ToList());
        }

        public ActionResult About()
        {
            return View(Question.DefaultQuestions());
        }

        public ActionResult Theme()
        {
            return View();
        }
    }
}
