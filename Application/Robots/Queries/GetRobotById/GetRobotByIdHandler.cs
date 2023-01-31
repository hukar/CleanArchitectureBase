namespace Application.Robots.Queries.GetRobotById;

public class GetRobotByIdHandler : IQueryHandler<GetRobotByIdQuery, GetRobotDto>
{
    private readonly IMapper _mapper;
    private readonly IRobotRepository _repo;

    public GetRobotByIdHandler(IMapper mapper, IRobotRepository repo)
    {
        _mapper = mapper;
        _repo = repo;
    }
    
    public async Task<GetRobotDto> Handle(GetRobotByIdQuery query, CancellationToken cancellationToken)
    {
        var robot = await _repo.GetById(query.Id);

        return _mapper.Map<GetRobotDto>(robot);
    }
}
