namespace Infrastructure.Persistence.Database;

public class DatabaseBootstrap
{
    private readonly DapperContext _context;
    private bool _isCreated = false;

    public DatabaseBootstrap(DapperContext context)
    {
        _context = context;
    }
    public void FillDb<TToInsert>(params TToInsert[] itemsToInsert)
        where TToInsert : ISaveableInDb
    {
        if(_isCreated == false)
        {
            Console.WriteLine("Use the method CreateDb first");
            return;
        }
    }
    public async void CreateDb()
    {
        var sql = "";
        var tables = new List<string>();
        
        if(IsTableExists("Robot") == false)
        {
            sql += @"CREATE TABLE Robot (
                        Id INTEGER PRIMARY KEY,
                        CodeName TEXT,
                        CreatedDate TEXT,
                        WeaponId INTEGER
                    );";

            tables.Add("Robot");
        }

        if(IsTableExists("Weapon") == false)
        {
            sql += @"CREATE TABLE Weapon (
                        Id INTEGER PRIMARY KEY,
                        Name TEXT
                    );";

            tables.Add("Weapon");
        }

        if(string.IsNullOrEmpty(sql) == false)
        {
            using var connection = _context.CreateConnection();

            await connection.ExecuteAsync(sql);
            _isCreated = true;
            Console.WriteLine($"table(s) {string.Join(" and ",tables)} are created");
        }
        
        Console.WriteLine("tables Robot and Weapon already exists");
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
