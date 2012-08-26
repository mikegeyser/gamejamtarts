using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scratch.XMLModels;
using gamejamtarts.Models;

namespace Scratch
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Db();

            foreach (var g in db.Games)
                db.Games.Remove(g);

            db.SaveChanges();

            foreach(var g in GameDTO.Games())
            {
                var game = new Game()
                    {
                        Title = g.Title,
                        Description = g.Description,
                        WordTheme = g.WordTheme,
                        ArtTheme = g.ArtTheme,
                        Url = g.Url,
                        CopyrightAndAttribution = g.CopyrightAndAttribution,
                        Code = g.Code,
                        
                    };
                game.Team = g.Team.Select(x => new Person() {Name = x}).ToList();
                game.Files = g.Contents.Select(x => new GameFile() {Url = x, Game = game}).ToList();
                game.Images = g.Images.Select(x => new Image() {Url = x, Game = game}).ToList();
                db.Games.Add(game);
            }

            db.SaveChanges();
        }
    }
}
