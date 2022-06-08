using System.Collections.Generic;
using System.Data.SQLite;

namespace database
{
  public interface IMapper
    {
        //interface til mapper
        List<Character> MapCharactersFromReader(SQLiteDataReader reader);
        List<Life> MapLiveFromReader(SQLiteDataReader reader);
        List<Musik> MapMusikFromReader(SQLiteDataReader reader);
    }
}
