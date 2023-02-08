namespace Infrastructure.Persistence.Database;

public class DatabaseBootstrap
{
    private readonly DapperContext _context;

    public DatabaseBootstrap(DapperContext context)
    {
        _context = context;
    }
    public void CreateDb()
    {
        var sql = @"CREATE TABLE Robot (
                        Id INTEGER,
                        CodeName TEXT,
                        CreatedDate 
                    )"
        using var connection = _context.CreateConnection();
    }

    private bool IsTableExists(string tableName)
    {
        using var connection = _context.CreateConnection();

        var sql = @"SELECT name
                    FROM sqlite_master
                    WHERE type='table' AND name=@Name";

        var tableNameInDB = connection.QueryFirstOrDefault(sql, new { Name = tableName });

        return tableNameInDB is not null;
    }
}
