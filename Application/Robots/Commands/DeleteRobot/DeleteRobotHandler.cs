using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Robots.Commands.DeleteRobot;

public class DeleteRobotHandler : ICommandHandler<DeleteRobotCommand>
{
    private readonly IRobotRepository _repo;

    public DeleteRobotHandler(IRobotRepository repo)
    {
        _repo = repo;
    }
    
    public Task<int> Handle(DeleteRobotCommand command, CancellationToken cancellationToken) => _repo.Delete(command.id);
}
