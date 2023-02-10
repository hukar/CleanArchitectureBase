namespace Application.Robots.Queries.GetAllRobots;

public class GetAllRobotsHandler : IQueryHandler<GetAllRobotsQuery, IEnumerable<GetRobotDto>>
{
    private readonly IRobotRepository _repo;
    private readonly IMapper _mapper;

    public GetAllRobotsHandler(IRobotRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetRobotDto>> Handle(GetAllRobotsQuery query, CancellationToken cancellationToken)
    {
        var robots = await _repo.GetAll();

        return _mapper.Map<List<GetRobotDto>>(robots);
    }
}
