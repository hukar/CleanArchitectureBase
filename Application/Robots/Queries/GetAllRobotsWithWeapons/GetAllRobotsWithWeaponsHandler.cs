namespace Application.Robots.Queries.GetAllRobotsWithWeapons;

public class GetAllRobotsWithWeaponsHandler : IQueryHandler<GetAllRobotsWithWeaponsQuery, IEnumerable<GetRobotWithWeaponsDto>>
{
    private readonly IMapper _mapper;
    private readonly IRobotRepository _repo;
    public GetAllRobotsWithWeaponsHandler(IRobotRepository repo, IMapper mapper)
    {
            _repo = repo;
            _mapper = mapper;   
    }

    public async Task<IEnumerable<GetRobotWithWeaponsDto>> Handle(GetAllRobotsWithWeaponsQuery requeryquest, CancellationToken cancellationToken)
    {
        var robots = await _repo.GetAllRobotsWithWeapons();

        return _mapper.Map<IEnumerable<GetRobotWithWeaponsDto>>(robots);
    }
}
