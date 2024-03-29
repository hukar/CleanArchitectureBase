namespace Infrastructure.Persistence.Repositories;

public class RobotRepositoryInMemory : IRobotRepository
{
    private readonly List<Robot> _robots = new() {
        new Robot {Id = 1, CodeName = "TT-99", CreatedAt = new DateTime(2016, 11, 23), Weapons = new List<Weapon> {
            new Weapon { Id = 5, Name = "Ice Sword" },
            new Weapon { Id = 18, Name = "Two Bullet Gun" },
            new Weapon { Id = 20, Name = "Tri Photonic Blaster" },
        }},
        new Robot {Id = 2, CodeName = "Z8-PT", CreatedAt = new DateTime(2018, 8, 12), Weapons = new List<Weapon>()},
        new Robot {Id = 3, CodeName = "BBB-3", CreatedAt = new DateTime(2018, 2, 21), Weapons = new List<Weapon> {
            new Weapon { Id = 12, Name = "Fire Blaster"},
            new Weapon { Id = 9, Name = "Double Silicium Axe"},
        }},
        new Robot {Id = 4, CodeName = "234-ACT", CreatedAt = new DateTime(2017, 6, 3), Weapons = new List<Weapon>()},
    };

    public async Task<IEnumerable<Robot>> GetAll()
    {
        await Task.Delay(500);

        return _robots;
    }

    public async Task<IEnumerable<Robot>> GetAllRobotsWithWeapons()
    {
        await Task.Delay(500);

         return _robots;
    }

    public async Task<Robot?> GetById(int id)
    {
        await Task.Delay(500);

        return _robots.SingleOrDefault(r => r.Id == id);
    }

    public async Task<Robot?> GetRobotWithWeaponsById(int id)
    {
        await Task.Delay(500);

        return _robots.SingleOrDefault(r => r.Id == id);
    }
    
    public async Task<Robot> Create(Robot robotToCreate)
    {
        await Task.Delay(500);

        robotToCreate.Id = _robots.Select(r => r.Id).DefaultIfEmpty(0).Max() + 1;

        _robots.Add(robotToCreate);
        return robotToCreate;
    }

    public async Task<int> Update(Robot robotToUpdate)
    {
        await Task.Delay(500);

        var robotToReplace = _robots.SingleOrDefault(r => r.Id == robotToUpdate.Id);

        if(robotToReplace is not null)
        {
            _robots.Remove(robotToReplace);
            _robots.Add(robotToUpdate);
            return 1; // rows affected
        }

        return 0; // rows affected
    }

    public async Task<int> Delete(int id)
    {
        await Task.Delay(500);

        var robotToDelete = _robots.SingleOrDefault(r => r.Id == id);

        if(robotToDelete is not null)
        {
             _robots.Remove(robotToDelete);
             return 1; // rows affected
        }

        return 0; // rows affected
    }

    public async Task<IEnumerable<Weapon>> GetAllWeapons()
    {
        await Task.Delay(500);
        
        return new List<Weapon> {
            new Weapon { Id = 5, Name = "Ice Sword" },
            new Weapon { Id = 18, Name = "Two Bullet Gun" },
            new Weapon { Id = 20, Name = "Tri Photonic Blaster" },
        };
    }

    public async Task<bool> IsWeaponExists(Weapon weapon) => await Task.FromResult(true);
}