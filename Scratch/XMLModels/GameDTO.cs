using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Scratch.XMLModels
{
    [Serializable, XmlRoot("Game")]
    public class GameDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string WordTheme { get; set; }
        public string ArtTheme { get; set; }

        [XmlArrayItem("Member")]
        public List<string> Team { get; set; }

        public string Url { get; set; }

        [XmlArrayItem("Item")]
        public List<string> Contents { get; set; }

        [XmlArrayItem("Url")]
        public List<string> Images { get; set; }
        public string CopyrightAndAttribution { get; set; }

        public string Code { get { return HttpUtility.HtmlEncode(Title.Replace("'", "").Replace(" ", "_")); } }
        public string TitleImage { get { return Images.FirstOrDefault(); } }
        public string ShortDescription
        {
            get
            {
                var abruptlyTruncatedText = Description.Substring(0, Math.Min(500, Description.Length));

                if (abruptlyTruncatedText.Length < 500)
                    return abruptlyTruncatedText; // Because it's the full description

                return abruptlyTruncatedText.Substring(0, abruptlyTruncatedText.LastIndexOf(" ")) + " (...)";
            }
        }

        private static List<GameDTO> games;
        public static List<GameDTO> Games()
        {
            if (games == null)
            {

                var url_base = @"http://digitalarts.wits.ac.za/jam/";
                games = new List<GameDTO>();
                var iser = new XmlSerializer(typeof(GameDTO));

                WebClient wc = new WebClient();
                var manifest = wc.DownloadString(url_base + "games_manifest.txt");
                var lines = manifest.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in lines)
                {
                    try
                    {
                        var game_xml = url_base + line;

                        var reader = new XmlTextReader(game_xml);
                        var o = iser.Deserialize(reader);

                        games.Add((GameDTO)o);
                    }
                    catch (Exception)
                    {
                        //swallow exception and
                        continue;
                    }



                }
            }

            return games;
        }
    }
}