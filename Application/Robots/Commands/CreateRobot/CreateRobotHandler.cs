namespace Application.Robots.Commands.CreateRobot;

public class CreateRobotHandler : ICommandHandler<CreateRobotCommand, GetRobotDto>
{
    private readonly IRobotRepository _repo;
    private readonly IMapper _mapper;

    public CreateRobotHandler(IRobotRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    
    public async Task<GetRobotDto> Handle(CreateRobotCommand command, CancellationToken cancellationToken)
    {
        var robotToCreate = _mapper.Map<Robot>(command.robotToCreate);

        var robotCreated = await _repo.Create(robotToCreate);

        return _mapper.Map<GetRobotDto>(robotCreated);
    }
}
