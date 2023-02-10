namespace Application.Robots.Queries.GetRobotWithWeaponsById;

public record GetRobotWithWeaponsByIdQuery(int Id) : IQuery<GetRobotWithWeaponsDto>;

