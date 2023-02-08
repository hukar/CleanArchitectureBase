namespace Infrastructure.Persistence.Database;

public class DapperContext
{
    private readonly string _connectionString;
    public DapperContext(IConfiguration configuration)
    {
        _connectionString = configuration!.GetConnectionString("connectionSqlite")!;
    }
    
    public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);
}
