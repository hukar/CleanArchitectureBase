namespace Application.Robots.Commands.UpdateRobot;

public record UpdateRobotCommand(int Id, CreateUpdateRobotDto RobotToUpdate) : ICommand;
