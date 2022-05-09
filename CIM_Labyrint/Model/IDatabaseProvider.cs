using System.Data;

namespace database
{
    public interface IDatabaseProvider
    {
        IDbConnection CreateConnection();

    }
}