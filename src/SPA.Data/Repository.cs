
using System.Collections.Generic;
using System.Data;
using Dapper;
using Npgsql;

public interface IRepository
{
    IEnumerable<dynamic> All();
}

public class Repository : IRepository
{
    public Repository()
    {
    }

    internal IDbConnection Connection
    {
        get
        {
            return new NpgsqlConnection("Host=localhost;Port=5432;Username=admin;Password=pass123;Database=spa_db");
        }
    }

    public IEnumerable<dynamic> All()
    {
        using (IDbConnection dbConnection = Connection)
        {
            dbConnection.Open();
            var result = dbConnection.Query<dynamic>("SELECT * FROM blog");
            return result;
        }
    }

}
