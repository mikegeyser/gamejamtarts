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
        public ActionResult Index()
        {
            return View(Game.SampleGames());
        }

        public ActionResult Details(string code)
        {
            var game = Game.Games().FirstOrDefault(x => x.Code == code);

            if (game == null)
                return this.RedirectToAction("Index");

            return View(game);
        }

        public ActionResult Reload()
        {
            Game.ResetGames();
            return null;
        }

    }
}