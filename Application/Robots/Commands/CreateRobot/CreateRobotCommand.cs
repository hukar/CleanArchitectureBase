namespace Application.Robots.Commands.CreateRobot;

public record CreateRobotCommand(CreateUpdateRobotDto robotToCreate) : ICommand<GetRobotDto>;

