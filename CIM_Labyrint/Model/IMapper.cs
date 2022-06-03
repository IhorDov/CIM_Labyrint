using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace database
{
    public interface IMapper
    {
        List<Character> MapCharactersFromReader(SQLiteDataReader reader);
        List<Life> MapLiveFromReader(SQLiteDataReader reader);

        List<Musik> MapMusikFromReader(SQLiteDataReader reader);



    }
}
