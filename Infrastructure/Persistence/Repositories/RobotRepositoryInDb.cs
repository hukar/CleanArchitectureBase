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

    public Task<Robot> Create(Robot entityToCreate)
    {
        throw new NotImplementedException();
    }

    public Task<int> Update(Robot entityToUpdate)
    {
        throw new NotImplementedException();
    }

    public Task<int> Delete(int id)
    {
        throw new NotImplementedException();
    }
}
