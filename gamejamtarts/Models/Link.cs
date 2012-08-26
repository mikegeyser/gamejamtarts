namespace gamejamtarts.Models
{
   
    public class Image   
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public Game Game { get; set; }

        public override string ToString()
        {
            return this.Url;
        }
    }

    public class GameFile
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public Game Game { get; set; }

        public override string ToString()
        {
            return this.Url;
        }
    }
}