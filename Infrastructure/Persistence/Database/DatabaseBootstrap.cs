namespace Infrastructure.Persistence.Database;

public class DatabaseBootstrap
{
    private readonly DapperContext _context;
    private bool _isCreated = false;

    public DatabaseBootstrap(DapperContext context)
    {
        _context = context;
    }

    public async Task CreateDb()
    {
        await DropTables();

        var sql = "";
        var tables = new List<string>();
        
        if(IsTableExists("Robot") == false)
        {
            sql += @"CREATE TABLE Robot (
                        Id INTEGER PRIMARY KEY,
                        CodeName TEXT,
                        CreatedAt TEXT,
                        CreatedBy TEXT,
                        ModifiedAt TEXT,
                        ModifiedBY
                    );";

            tables.Add("Robot");
        }

        if(IsTableExists("Weapon") == false)
        {
            sql += @"CREATE TABLE Weapon (
                        Id INTEGER PRIMARY KEY,
                        Name TEXT,
                        RobotID INTEGER
                    );";

            tables.Add("Weapon");
        }

        if(string.IsNullOrEmpty(sql) == false)
        {
            using var connection = _context.CreateConnection();

            await connection.ExecuteAsync(sql);
            _isCreated = true;
            Console.WriteLine($"table(s) {string.Join(" and ",tables)} are created");
            return;
        }
        
        Console.WriteLine("tables Robot and Weapon already exists");
    }

    public async Task FillDb()
    {
        await Task.WhenAll(
            FillRobotDb(
                new Robot { CodeName = "JO-JO"},
                new Robot { CodeName = "VBG-67"},
                new Robot { CodeName = "MICH-3L"}
            ), 
            FillWeaponDb(
                new Weapon { Name = "Light Saber Blue", RobotId = 1},
                new Weapon { Name = "Ultra Sword Of Fire", RobotId = 2},
                new Weapon { Name = "Radical Blaster Gen II", RobotId = 2}
            )
        );
    }
    private async Task FillRobotDb(params Robot[] robotsToInsert)
    {
        if(_isCreated == false)
        {
            Console.WriteLine("Use the method CreateDb first");
            return;
        }

        var sql = @"INSERT INTO Robot (CodeName, CreatedAt, CreatedBy)
                    VALUES (@CodeName, @CreatedAt, @CreatedBy)";

        foreach(var robot in robotsToInsert)
        {
            robot.CreatedAt = DateTime.Now;
            robot.CreatedBy = "System";
        }
        
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, robotsToInsert);

        Console.WriteLine($"{robotsToInsert.Count()} robots was inserted successfuly");
    }

    private async Task FillWeaponDb(params Weapon[] weaponsToInsert)
    {
        if(_isCreated == false)
        {
            Console.WriteLine("Use the method CreateDb first");
            return;
        }

        var sql = @"INSERT INTO Weapon (Name, RobotId)
                    VALUES (@Name, @RobotId)";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, weaponsToInsert);

        Console.WriteLine($"{weaponsToInsert.Count()} weapons was inserted successfuly");
    }

    private async Task DropTables()
    {
        var sql = @"DROP TABLE IF EXISTS Robot;
                    DROP TABLE IF EXISTS Weapon";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql);
        Console.WriteLine("Tables Robot and Weapon are dropped");
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
