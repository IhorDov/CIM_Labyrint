using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace database
{
    public class AdventurerMapper : IAdventurerMapper
    {

        public List<Character> MapCharactersFromReader(SQLiteDataReader reader)
        {


            var result = new List<Character>();


            while (reader.Read())
            {
             
                result.Add(new Character() { Buget = 1000000 });
            }
            return result;
        }

    }
}
