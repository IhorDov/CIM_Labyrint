using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace database
{
    public class Reading : IAdventurerRepository
    {

        public readonly IDatabaseProvider provider;
        private readonly IAdventurerMapper mapper;
        private IDbConnection connection;



        public Reading(IDatabaseProvider provider, IAdventurerMapper mapper)
        {
            this.provider = provider;
            this.mapper = mapper;
        }

        public void CreateDatabaseTables()
        {



            var createAdministrator = new SQLiteCommand($"drop table if exists Administrator");


            //CREATE TABLES IF NOT EXISTS 
            createAdministrator = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Administrator (Id INTEGER PRIMARY KEY, Buget VARCHAR(50));", (SQLiteConnection)connection);


            createAdministrator.ExecuteNonQuery();


        }

        public void AddAdmin(int Buget)
        {
            var cmd = new SQLiteCommand($"INSERT INTO Administrator (Buget) VALUES ({Buget})", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public Character GetAllAdmin(int Buget)
    {
        var cmd = new SQLiteCommand($"SELECT * from Administrator WHERE Buget = '{Buget}'", (SQLiteConnection)connection);
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
