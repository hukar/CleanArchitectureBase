using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Robots.Queries.IsWeaponExists;

public class IsWeaponExistsHandler : IQueryHandler<IsWeaponExistsQuery, bool>
{
    private readonly IRobotRepository _repo;
    private readonly IMapper _mapper;

    public IsWeaponExistsHandler(IRobotRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    
    public async Task<bool> Handle(IsWeaponExistsQuery query, CancellationToken cancellationToken)
    {
        var weapon = _mapper.Map<Weapon>(query.weaponToCheck);
        var isWeaponExists = await _repo.IsWeaponExists(weapon);

        return isWeaponExists;
    }
}
