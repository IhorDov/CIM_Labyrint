using System;
using System.Collections.Generic;
using System.Text;

namespace database
{
   public interface IAdventurerRepository
    {
        void AddAdmin(int buget);

        void AddTornament(string tournamentName, string country, int amountOfTeam, int money);

        void AddTeam(int cvrNumber, string teamsName);

        void AddRider(int cprNumber, string firstName, string lastName, int abilities, int power, int endurance, int speed, int price, int teamCVRnumber);

        //Character FindCharacter(string name);
         Character GetAllAdmin(int buget);

        

        void Open();

        void Close();
    }
}
