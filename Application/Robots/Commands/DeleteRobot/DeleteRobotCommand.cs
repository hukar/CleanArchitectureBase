using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Robots.Commands.DeleteRobot;

public record DeleteRobotCommand(int id) : ICommand;
