using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace database
{
    public class Reading : IAdventurerRepository
    {

        private readonly IDatabaseProvider provider;
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
            var createTournament = new SQLiteCommand($"drop table if exists Tournament");
            var createTeam = new SQLiteCommand($"drop table if exists Team");
            var createRider = new SQLiteCommand($"drop table if exists Rider");

            //CREATE TABLES IF NOT EXISTS 
            createAdministrator = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Administrator (Id INTEGER PRIMARY KEY, Buget VARCHAR(50));", (SQLiteConnection)connection);

             createTeam = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Team (CVRnumber, Teamsname TINYTEXT, AdministratorId Integer, foreign key (AdministratorId) references Administrator(Id));", (SQLiteConnection)connection);

             createTournament = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Tournament (TournamentName TINYTEXT, Country TINYTEXT, AmountOfTeam VARCHAR(30), Money VARCHAR(50), TeamCVRnumber integer, foreign key (TeamCVRnumber) references Team (CVRnumber));", (SQLiteConnection)connection);
             createRider = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Rider(CPRnumber INTEGER PRIMARY KEY, FirstName TINYTEXT, LastName TINYTEXT, Abilities Integer, Power Integer, Endurance Integer, Speed Integer, Price integer, TeamCVRnumber integer, foreign key (TeamCVRnumber) references Team (CVRnumber));", (SQLiteConnection)connection);

            createAdministrator.ExecuteNonQuery();
            createTeam.ExecuteNonQuery();
            createRider.ExecuteNonQuery();
            createTournament.ExecuteNonQuery();

        }


        

        public void AddTeam(int CVRnumber, string Teamsname)
        {
            var cmd = new SQLiteCommand($"INSERT INTO Team (CVRnumber,Teamsname) VALUES ('{CVRnumber}','{Teamsname}')", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public void AddTornament(string tournamentName, string country, int amountOfTeam, int money)
        {
            var cmd = new SQLiteCommand($"INSERT INTO Tournament (TournamentName, Country, AmountOfTeam, Money) VALUES ('{tournamentName}', '{country}', {amountOfTeam}, {money})", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public void AddRider(int cprNumber, string firstName, string lastName, int abilities, int power, int endurance, int speed, int price, int teamCVRnumber)
        {
            var cmd = new SQLiteCommand($"INSERT INTO Rider (CPRnumber, FirstName, LastName, Abilities, Power, Endurance, Speed, Price, TeamCVRnumber) VALUES ({cprNumber}, '{firstName}', '{lastName}', {abilities}, {power}, {endurance}, {speed}, {price}, {teamCVRnumber})", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public void AddAdmin(int Buget)
        {
            var cmd = new SQLiteCommand($"INSERT INTO administrator (Buget) VALUES ({Buget})", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public void Addteam(string teamsname , int cvrnumber)
        {
            var cmd = new SQLiteCommand($"INSERT INTO team (cvrnumber,teamsname) VALUES ('{cvrnumber}' , '{teamsname}')", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public void Add(string teamsname, int cvrnumber)
        {
            var cmd = new SQLiteCommand($"INSERT INTO team (cvrnumber,teamsname) VALUES ('{cvrnumber}' , '{teamsname}')", (SQLiteConnection)connection);
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
