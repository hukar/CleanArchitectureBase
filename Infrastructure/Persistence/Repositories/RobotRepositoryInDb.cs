namespace Infrastructure.Persistence.Repositories;

public class RobotRepositoryInDb : IRobotRepository
{
    private readonly DapperContext _context;
    
    public RobotRepositoryInDb(DapperContext context)
    {
        _context = context;   
    }

    public async Task<IEnumerable<Robot>> GetAll()
    {
        var sql = @"SELECT * FROM Robot";

        using var connection = _context.CreateConnection();
        var robots = await connection.QueryAsync<Robot>(sql);
        return robots;
    }
    
    public async Task<IEnumerable<Robot>> GetAllRobotsWithWeapons()
    {
        var sql = @"SELECT * 
                    FROM Robot as r
                    LEFT JOIN Weapon as w
                    ON w.RobotId = r.Id";

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<Robot, Weapon, Robot>(sql, (robot, weapon) => {
            if(weapon is not null) robot.Weapons.Add(weapon);
            return robot;
        });

        var robots = result.GroupBy(robot => robot.Id).Select(grp => {
            var robot = grp.First();
            var weapons = grp
                            .Where(r => r.Weapons.Count > 0)
                            .Select(r =>  r.Weapons.First()).ToList();
            robot.Weapons = weapons;
            
            return robot;
        });

        return robots;
    }

    public async Task<Robot?> GetById(int id)
    {
        var sql = @"SELECT *
                    FROM Robot
                    WHERE Id = @id";
        
        using var connection = _context.CreateConnection();

        var robot = await connection.QuerySingleOrDefaultAsync<Robot>(sql, new { id });

        return robot;
    }

    public async Task<Robot?> GetRobotWithWeaponsById(int id)
    {
        var sql = @"SELECT * FROM Robot WHERE Id = @id;
                    SELECT * FROM Weapon WHERE RobotId = @id;";

        using var connection = _context.CreateConnection();

        var gridReader = await connection.QueryMultipleAsync(sql, new { id });

        var robot = await gridReader.ReadSingleOrDefaultAsync<Robot>();
        var weapons = await gridReader.ReadAsync<Weapon>();

        if(robot is not null) robot.Weapons = weapons.ToList();

        return robot;
    }

    public async Task<Robot> Create(Robot entityToCreate)
    {
        var sql = @"INSERT INTO Robot ( CodeName, CreatedAt, CreatedBY )
                    VALUES ( @CodeName, @CreatedAt, @CreatedBy );
                    SELECT last_insert_rowid();";

        using var connection = _context.CreateConnection();

        entityToCreate.CreatedAt = DateTime.Now;
        entityToCreate.CreatedBy = "SYSTEM";

        var lastInsertedId = await connection.ExecuteScalarAsync<int>(sql, entityToCreate);

        entityToCreate.Id = lastInsertedId;

        return entityToCreate;
    }

    public async Task<int> Update(Robot entityToUpdate)
    {
        var sql = @"UPDATE Robot
                    SET 
                        CodeName = @CodeName,
                        ModifiedAt = @ModifiedAt,
                        ModifiedBy = @ModifiedBy
                    WHERE Id = @Id";

        using var connection = _context.CreateConnection();

        entityToUpdate.ModifiedAt = DateTime.Now;
        entityToUpdate.ModifiedBy = "SYSTEM";

        var rowsAffected = await connection.ExecuteAsync(sql, entityToUpdate);

        return rowsAffected; 
    }

    public async Task<int> Delete(int id)
    {
        var sql = @"DELETE 
                    FROM Robot
                    WHERE Id = @id";

        using var connection = _context.CreateConnection();

        var rowsAffected = await connection.ExecuteAsync(sql, new { id });

        return rowsAffected;
    }
}
