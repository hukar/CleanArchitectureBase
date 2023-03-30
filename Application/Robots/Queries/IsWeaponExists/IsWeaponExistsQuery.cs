using Application.Interfaces.CQRS;

namespace Application.Robots.Queries.IsWeaponExists;

public record IsWeaponExistsQuery(GetWeaponDto weaponToCheck) : IQuery<bool>;

