using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace database
{
    public class Reading : IRepository
    {

        public readonly IDatabaseProvider provider;
        private readonly IMapper mapper;
        private IDbConnection connection;

        public Reading(IDatabaseProvider provider, IMapper mapper)
        {
            //connection
            this.provider = provider;
            //udskriver på skærmen
            this.mapper = mapper;
        }

        public void CreateDatabaseTables()
        {


            //CREATE TABLES IF NOT EXISTS 
            var score = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Score (Id INTEGER PRIMARY KEY, Score VARCHAR(100));", (SQLiteConnection)connection);
            var musik = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Musik (Id INTEGER PRIMARY KEY, T BLOB);", (SQLiteConnection)connection);
           var lifeDel = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Life (Id INTEGER PRIMARY KEY, life VARCHAR(100));", (SQLiteConnection)connection);

            
            //start SQL
            score.ExecuteNonQuery();
lifeDel.ExecuteNonQuery();
            musik.ExecuteNonQuery();

        }
        public void Addmusik(bool T)
        {
            var cmd = new SQLiteCommand($"INSERT INTO Musik (T) VALUES ({T})", (SQLiteConnection)connection);
            //den er variabel bruger vi til at kunne starte en INSERT i en tabel
            cmd.ExecuteNonQuery();
        }

        public void AddLife(int life)
        {
            var cmd = new SQLiteCommand($"INSERT INTO Life (life) VALUES ({life})", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public void UPDATELife(int life)
        {
            var cmd = new SQLiteCommand($"UPDATE Life SET life = ({life}) WHERE Id = 1", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }
        //UPDATE COMPANY SET ADDRESS = 'Texas' WHERE ID = 6;

        public void AddScore(int Score)
        {
            var cmd = new SQLiteCommand($"INSERT INTO Score (Score) VALUES ({Score})", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        // alle sammen søger igennem mit ID på Spilleren

        public Life GetAllLife(int Id)
        {
            // vi bruger den når vi skal bruge et Select
            var cmd = new SQLiteCommand($"SELECT * from Life WHERE Id = '{Id}'", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();

            var result = mapper.MapLiveFromReader(reader).First();
            return result;
        }


        public Musik GetAlltru(int Id)
        {
            // vi bruger den når vi skal bruge et Select
            var cmd = new SQLiteCommand($"SELECT * from Musik WHERE Id = '{Id}'", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();

            var result = mapper.MapMusikFromReader(reader).FirstOrDefault();
            return result;
        }

        public Character GetAllScore(int Id)
        {
            // vi bruger den når vi skal bruge et Select
            var cmd = new SQLiteCommand($"SELECT * from Score WHERE Id = '{Id}'", (SQLiteConnection)connection);
            // vi bruger den til at starte commands i SQL
            var reader = cmd.ExecuteReader();
            // Vi tager bare resultatet fra databasen og tager den først i row 1
            var result = mapper.MapCharactersFromReader(reader).First();
            return result;
        }



        public void Open()
        {
            //   vi tjekker om connection er 0 hvis den er 0  så laver vi en connection
            if (connection == null)
            {
                connection = provider.CreateConnection();
            }
            connection.Open();
            // Når alt er gået godt og den er startet på den rigtige måde så begynder den at lave også tabeller
            CreateDatabaseTables();
        }
        public void Close()
        {
            // vi bruger denne her for at stoppe memory leak da det sker hvis vi ikke stopper når vi er færdig så er det bedre vi starter og stopper den når vi skal bruge det for at være sikker
            connection.Close();
        }
    }
}
