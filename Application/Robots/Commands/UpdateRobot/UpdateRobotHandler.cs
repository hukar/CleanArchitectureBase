namespace Application.Robots.Commands.UpdateRobot;

public class UpdateRobotHandler : ICommandHandler<UpdateRobotCommand>
{
        private readonly IRobotRepository _repo;
        private readonly IMapper _mapper;
    public UpdateRobotHandler(IRobotRepository repo, IMapper mapper)
    {
            _mapper = mapper;
            _repo = repo;  
    }
    
    public async Task<int> Handle(UpdateRobotCommand command, CancellationToken cancellationToken) => await _repo.Update(_mapper.Map<Robot>(command.RobotToUpdate));

}
