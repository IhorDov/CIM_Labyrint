using System.Collections.Generic;
using System.Data.SQLite;

namespace database
{
    public class Mapper : IMapper
    {
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
    }
}