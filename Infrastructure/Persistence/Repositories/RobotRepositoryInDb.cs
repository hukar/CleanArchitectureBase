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
            robot.Weapons.Add(weapon);
            return robot;
        });

        var robots = result.GroupBy(robot => robot.Id).Select(grp => {
            var robot = grp.First();
            foreach(var r in grp)
            {
                if(r.Weapons.Count > 0) robot.Weapons.Add(r.Weapons.First());
            }
            return robot;
        });

        return robots;
    }

    public Task<Robot?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Robot?> GetRobotWithWeaponsById(int id)
    {
        throw new NotImplementedException();
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
