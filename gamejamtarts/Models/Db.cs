using MongoDB.Driver;

namespace gamejamtarts.Models
{
    public static class Db
    {
        private static MongoDatabase db;

        static Db()
        {
            db = MongoServer.Create("mongodb://jammer:DogBatteryKettle@ds031947.mongolab.com:31947/gamejamtarts")
                            .GetDatabase("gamejamtarts");
        }

        public static MongoCollection<Game> Games()
        {
            return db.GetCollection<Game>("games");
        }
    }
}