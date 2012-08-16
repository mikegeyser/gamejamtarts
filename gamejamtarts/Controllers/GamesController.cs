using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using gamejamtarts.Models;

namespace gamejamtarts.Controllers
{
    public class GamesController : Controller
    {
        private Db db = new Db();

        public ActionResult Index()
        {
            return View(db.Games.ToList());
        }

        public ActionResult Details(string code)
        {
            var game = db.Games.FirstOrDefault(x => x.Code == code);

            if (game == null)
                return this.RedirectToAction("Index");

            return View(game);
        }

        public ActionResult Reload()
        {
            Game.ResetGames();
            Game.InitialiseGames();
            return null;
        }

    }
}