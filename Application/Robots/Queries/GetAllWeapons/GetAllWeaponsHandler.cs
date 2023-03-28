namespace Application.Robots.Queries.GetAllWeapons;

public class GetAllWeaponsHandler : IQueryHandler<GetAllWeaponsQuery, IEnumerable<GetWeaponDto>>
{
    private readonly IRobotRepository _repo;
    private readonly IMapper _mapper;

    public GetAllWeaponsHandler(IRobotRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetWeaponDto>> Handle(GetAllWeaponsQuery query, CancellationToken cancellationToken)
    {
        var weapons = await _repo.GetAllWeapons();

        return _mapper.Map<IEnumerable<GetWeaponDto>>(weapons);
    }
}

