using System.Collections.Generic;
using System.Data.SQLite;

namespace database
{
    public class Mapper : IMapper
    {
        //denne klasse er bare get og set

        /// <summary>
        /// det er den der sørger for at udskrive tingene på skærmen for eksempel som du kan se ved ID så tager den ID fra databasen
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<Character> MapCharactersFromReader(SQLiteDataReader reader)
        {
            var result = new List<Character>();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var name = reader.GetString(1);

                result.Add(new Character() { Id = id, Score = name });
            }
            return result;
        }

        public List<Life> MapLiveFromReader(SQLiteDataReader reader)
        {
            var result = new List<Life>();
            while (reader.Read())
            {
                var idthis = reader.GetInt32(0);
                var lifethis = reader.GetString(1);

                result.Add(new Life() { Id = idthis, life = lifethis });
            }
            return result;
        }


        public List<Musik> MapMusikFromReader(SQLiteDataReader reader)
        {
            var result = new List<Musik>();
            while (reader.Read())
            {
                var lifethis = reader.GetBoolean(0);

                var idthis = reader.GetInt32(1);

                result.Add(new Musik() { Id = idthis, T = lifethis });
            }
            return result;
        }


    }
}