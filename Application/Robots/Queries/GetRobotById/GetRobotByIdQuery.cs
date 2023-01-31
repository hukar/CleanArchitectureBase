namespace Application.Robots.Queries.GetRobotById;

public record GetRobotByIdQuery(int Id) : IQuery<GetRobotDto>;

