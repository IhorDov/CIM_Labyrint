using System;
using System.Collections.Generic;
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
            this.provider = provider;
            this.mapper = mapper;
        }

        public void CreateDatabaseTables()
        {



            //var Score = new SQLiteCommand($"drop table if exists Score");


            //CREATE TABLES IF NOT EXISTS 
           var Score = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Score (Id INTEGER PRIMARY KEY, Score VARCHAR(100));", (SQLiteConnection)connection);


            Score.ExecuteNonQuery();


        }

        public void AddScore(int Score)
        {
            var cmd = new SQLiteCommand($"INSERT INTO Score (Score) VALUES ({Score})", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public Character GetAllScore(int Id)
    {
            var cmd = new SQLiteCommand($"SELECT * from Score WHERE Id = '{Id}'", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();

            var result = mapper.MapCharactersFromReader(reader).First();
            return result;
        }


        public void Open()
        {

            if (connection == null)
            {
                connection = provider.CreateConnection();
            }
            connection.Open();

            CreateDatabaseTables();
        }

        public void Close()
        {
            connection.Close();
        }


    }
}
