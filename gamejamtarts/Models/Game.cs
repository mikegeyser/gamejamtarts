using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace gamejamtarts.Models
{
    [Serializable]
    public class Game
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string WordTheme { get; set; }
        public string ArtTheme { get; set; }

        [XmlArrayItemAttribute("Member")]
        public List<string> Team { get; set; }
        
        public string Url { get; set; }

        [XmlArrayItemAttribute("Item")]
        public List<string> Contents { get; set; }
        
        [XmlArrayItemAttribute("Url")]
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

        private static List<Game> games;
        public static List<Game> Games() 
        {
            if (games == null)
            {
                var url_base = @"http://www.digitalarts.wits.ac.za/jam/";
                games = new List<Game>();
                var iser = new XmlSerializer(typeof (Game));

                WebClient wc = new WebClient();
                var manifest = wc.DownloadString(url_base + "games_manifest.txt");
                var lines = manifest.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in lines)
                {
                    try
                    {
                        var game_xml = url_base + line;

                        var reader = new XmlTextReader(game_xml);
                        var o = iser.Deserialize(reader);

                        games.Add((Game) o);
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

        public static void ResetGames()
        {
            games = null;
        }

        public static List<Game> SampleGames()
        {
            // Pac Man
            var one = new Game();
            one.Title = "Pac-Man (Example)";
            one.Description = @"(Example) The player controls Pac-Man through a maze, eating pac-dots or pellets. When all dots are eaten, Pac-Man is taken to the next stage, between some stages one of three intermission animations plays.Four enemies (Blinky, Pinky, Inky and Clyde) roam the maze, trying to catch Pac-Man. If an enemy touches Pac-Man, a life is lost. When all lives have been lost, the game ends. Pac-Man is awarded a single bonus life at 10,000 points by default—DIP switches inside the machine can change the required points or disable the bonus life altogether. Near the corners of the maze are four larger, flashing dots known as power pellets that provide Pac-Man with the temporary ability to eat the enemies. The enemies turn deep blue, reverse direction and usually move more slowly. When an enemy is eaten, its eyes remain and return to the center box where it is regenerated in its normal color. Blue enemies flash white before they become dangerous again and the length of time for which the enemies remain vulnerable varies from one stage to the next, generally becoming shorter as the game progresses. In later stages, the enemies go straight to flashing, bypassing blue, although they still reverse direction when a power pellet is eaten.";
            one.WordTheme = "Cookies?!";
            one.ArtTheme = "These are all the colours we had!";
            one.Team = new List<string>
                           {
                               "Tōru Iwatani – Game designer",
                                "Shigeo Funaki (舟木茂雄) – Programmer",
                                "Toshio Kai (甲斐敏夫) – Sound & Music"
                           };
            one.Url = "http://en.wikipedia.org/wiki/Pac-Man";
            one.Contents = new List<string>
                               {
                                   "Game files",
                                   "Player manual",
                                   "Cookies"
                               };

            one.Images = new List<string>
                             {
                                 "http://upload.wikimedia.org/wikipedia/en/5/59/Pac-man.png",
                                 "http://upload.wikimedia.org/wikipedia/en/5/51/Pacman_title_na.png",
                                 "http://upload.wikimedia.org/wikipedia/en/d/da/Pac-Man_split-screen_kill_screen.png",
                                 "http://upload.wikimedia.org/wikipedia/en/1/16/Pac_flyer.png"
                             };
            one.CopyrightAndAttribution = "All rights of this work belong to Namco Bandai, images included here are from wikipedia. This is just an example, please don't sue me.";

            // Monkey Island
            var two = new Game();
            two.Title = "The Secret of Monkey Island (Example)";
            two.Description = @"(Example) The Secret of Monkey Island is a 2D adventure game played from a third-person perspective. Via a point-and-click interface, the player guides protagonist Guybrush Threepwood through the game's world and interacts with the environment by selecting from twelve verb commands (nine in newer versions) such as 'talk to' for communicating with characters and 'pick up' for collecting items between commands and the world's objects in order to successfully solve puzzles and thus progress in the game.[4] While conversing with other characters, the player may choose between topics for discussion that are listed in a dialog tree; the game is one of the first to incorporate such a system.[5] The in-game action is frequently interrupted by cutscenes, non-interactive animated sequences that are used to provide information about character personalities and advance the plot.[6] Like other LucasArts adventure games, The Secret of Monkey Island features a design philosophy that makes the player character's death impossible.";
            two.WordTheme = "Pirates!";
            two.ArtTheme = "Awesomeness wrapped in win. (We had some colours left over).";
            two.Team = new List<string>
                           {
                               "Ron Gilbert (Designer and Writer)",
                                "Dave Grossman (Writer)",
                                "Tim Schafer (Writer)",
                                "Michael Land (Composer)"
                           };
            two.Url = "http://en.wikipedia.org/wiki/The_Secret_of_Monkey_Island";
            two.Contents = new List<string>
                               {
                                   "Game files",
                                   "Player manual",
                                   "List of randomly generated insults. (Warning: some may be bad)",
                                   "Grog Recipe"
                               };

            two.Images = new List<string>
                             {
                                 "http://upload.wikimedia.org/wikipedia/en/9/9f/The_Secret_of_Monkey_Island_SCUMM_Bar.jpg",
                                 "http://upload.wikimedia.org/wikipedia/en/2/28/The_Secret_of_Monkey_Island_Special_Edition_SCUMM_Bar.jpg",
                                 "http://upload.wikimedia.org/wikipedia/en/a/a8/The_Secret_of_Monkey_Island_artwork.jpg"
                             };
            two.CopyrightAndAttribution = "All rights of this work belong to LucasArts, images included here are from wikipedia. This is just an example, please don't sue me.";

            // 
            var three = new Game();
            three.Title = "Sissy's Magical Ponycorn Adventure";
            three.Description = @"(Example) The game opens with Sissy declaring her love for a fictional species called 'ponycorns', and inviting the player to join her on her quest to collect the five ponycorns. She travels through rainbows to reach new areas; when she goes through the first one, she finds a person named OrangeBoy, who speaks in murmurs and gives her jars to put ponycorns in. She enters other rainbows and receives ponycorns through various tasks, which include flipping a turtle over and receiving one as a reward, saving one from a cage by turning a dinosaur into a mouse, saving one from a hungry tiger, and defeating a talking lemon with a coconut. Once she collects four, she returns to OrangeBoy's location, only to discover that he was a ponycorn in disguise who wanted to make sure that she would be nice to ponycorns. She puts him in a jar, and she expresses joy over finding the ponycorns. The game ends by telling players that the tiger had become nice, the lemon remained evil, the dinosaur remained a mouse, and the turtle was happy.";
            three.WordTheme = "Need I say more than the unholy hybrid of ponies and unicorns?";
            three.ArtTheme = "Whatever crayola's I brought with me..";
            three.Team = new List<string>
                             {
                                 "Ryan Henson Creighton",
                                 "Cassie Creighton"
                             };
            three.Url = "http://ponycorns.com";
            three.Contents = new List<string>
                               {
                                   "Hosted game",
                                   "Sparkles",
                                   "Goodwill to all mankind",
                                   "Oh, and pnycorns"
                               };

            three.Images = new List<string>
                             {
                                 "http://upload.wikimedia.org/wikipedia/en/f/f8/Sissy%27s_Magical_Ponycorn_Adventure.png",
                                 "http://upload.wikimedia.org/wikipedia/en/3/3b/Sissy%27s_Magical_Ponycorn_Adventure_screenshot.png"
                             };
            three.CopyrightAndAttribution = "All rights of this work belong to Untold Entertainment Inc, which I think is probably a trust fund that Cassie's dad set up for her university educations. Yes, ponycorns will ensure that she has a tertiary education. This is just an example, please don't sue me.";
            return new List<Game>(){one, two, three};
        }
    }
}