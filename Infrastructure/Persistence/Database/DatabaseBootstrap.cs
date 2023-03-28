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
        Console.WriteLine("Start create DB");
        Console.WriteLine("Drop Tables Before");
        await DropTables();
        Console.WriteLine("Drop Tables After");

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
                        ModifiedBy TEXT
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

        if(IsTableExists("RobotWeapon") == false)
        {
            sql += @"CREATE TABLE RobotWeapon (
                        RobotId INTEGER NOT NULL,
                        WeaponId INTEGER NOT NULL,
                        PRIMARY KEY (RobotId, WeaponId)
                        FOREIGN KEY (RobotId) REFERENCES Robot (Id)
                            ON DELETE CASCADE ON UPDATE NO ACTION
                        FOREIGN KEY (WeaponID) REFERENCES Weapon (Id)
                            ON DELETE CASCADE ON UPDATE NO ACTION
            )";

            tables.Add("RobotWeapon");
        }

        if(string.IsNullOrEmpty(sql) == false)
        {
            using var connection = _context.CreateConnection();

            Console.WriteLine("Create Table(s) Start");
            await connection.ExecuteAsync(sql);
            _isCreated = true;
            Console.WriteLine($"table(s) {string.Join(" and ",tables)} are created");
            return;
        }
        else
        {
            Console.WriteLine("tables for Robot and Weapon already exists");
        }  
    }

    public async Task FillDb()
    {
        await Task.WhenAll(
            FillRobotDb(
                new Robot { CodeName = "JO-JO" },
                new Robot { CodeName = "VBG-67" },
                new Robot { CodeName = "MICH-3L" },
                new Robot { CodeName = "AM0-R" },
                new Robot { CodeName = "BB-8" },
                new Robot { CodeName = "3L-SA" },
                new Robot { CodeName = "OL1-0" },
                new Robot { CodeName = "RAY-M0" },
                new Robot { CodeName = "T0T0" },
                new Robot { CodeName = "RR-TT" },
                new Robot { CodeName = "FIN-AL" },
                new Robot { CodeName = "AN-ANAS" },
                new Robot { CodeName = "R2-D2" }
            ), 
            FillWeaponDb(
                new Weapon { Name = "Little Fire Gun" },
                new Weapon { Name = "Blaster Of Fire" },
                new Weapon { Name = "Mini Poison Phaser" },
                new Weapon { Name = "Death Poison Blaster" },
                new Weapon { Name = "Fire Light Saber" },
                new Weapon { Name = "Green Short Saber" }
            ),
            FillRobotWeaponJoinDb(
                new RobotWeaponJoin(1,1),
                new RobotWeaponJoin(2,1),
                new RobotWeaponJoin(2,2),
                new RobotWeaponJoin(4,1),
                new RobotWeaponJoin(4,2),
                new RobotWeaponJoin(4,3),
                new RobotWeaponJoin(5,4),
                new RobotWeaponJoin(7,5),
                new RobotWeaponJoin(7,4),
                new RobotWeaponJoin(8,3),
                new RobotWeaponJoin(9,3),
                new RobotWeaponJoin(9,2)
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

        Console.WriteLine($"{robotsToInsert.Length} robots was inserted successfuly");
    }
    private async Task FillRobotWeaponJoinDb(params RobotWeaponJoin[] robotWeaponJoinToInsert)
    {
        if(_isCreated == false)
        {
            Console.WriteLine("Use the method CreateDb first");
            return;
        }

        var sql = @"INSERT INTO RobotWeapon (RobotId, WeaponId)
                    VALUES (@RobotId, @WeaponId)";

        
        
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, robotWeaponJoinToInsert);

        Console.WriteLine($"{robotWeaponJoinToInsert.Length} Robot-weapon Join was inserted successfuly");
    }

    private async Task FillWeaponDb(params Weapon[] weaponsToInsert)
    {
        if(_isCreated == false)
        {
            Console.WriteLine("Use the method CreateDb first");
            return;
        }

        var sql = @"INSERT INTO Weapon (Name)
                    VALUES (@Name)";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, weaponsToInsert);

        Console.WriteLine($"{weaponsToInsert.Length} weapons was inserted successfuly");
    }

    private async Task DropTables()
    {
        Console.WriteLine("Start Drop Tables");
        using var connection = _context.CreateConnection();

        var sql = "DROP TABLE IF EXISTS RobotWeapon";
        await connection.ExecuteAsync(sql);
        Console.WriteLine("RobotWeapon is Dropped");

        sql = @"DROP TABLE IF EXISTS Robot;
                    DROP TABLE IF EXISTS Weapon;";

        
        await connection.ExecuteAsync(sql);
        Console.WriteLine("Tables Robot and Weapon are dropped");

        Console.WriteLine("Finish Drop Tables");
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
