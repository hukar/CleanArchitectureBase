namespace Application.Interfaces.Repositories;

public interface IRobotRepository : IRepositoryAsync<Robot>
{
    Task<List<Robot>> GetAllRobotsWithWeapon();
    Task<Robot?> GetRobotByIdWithWeapon(int id);
}
