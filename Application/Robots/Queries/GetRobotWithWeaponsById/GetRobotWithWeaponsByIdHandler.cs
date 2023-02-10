namespace Application.Robots.Queries.GetRobotWithWeaponsById;

public class GetRobotWithWeaponsByIdHandler : IQueryHandler<GetRobotWithWeaponsByIdQuery, GetRobotWithWeaponsDto>
{
    private readonly IRobotRepository _repo;
    private readonly IMapper _mapper;

    public GetRobotWithWeaponsByIdHandler(IRobotRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    
    public async Task<GetRobotWithWeaponsDto> Handle(GetRobotWithWeaponsByIdQuery query, CancellationToken cancellationToken)
    {
        var robot = await _repo.GetRobotWithWeaponsById(query.Id);

        return _mapper.Map<GetRobotWithWeaponsDto>(robot);
    }
}
