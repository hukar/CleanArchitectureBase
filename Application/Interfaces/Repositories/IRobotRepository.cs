namespace Application.Interfaces.Repositories;

public interface IRobotRepository : IRepositoryAsync<Robot>
{
    Task<IEnumerable<Robot>> GetAllRobotsWithWeapons();
    Task<Robot?> GetRobotWithWeaponsById(int id);

    Task<IEnumerable<Weapon>> GetAllWeapons();
}
