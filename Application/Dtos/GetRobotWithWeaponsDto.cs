namespace Application.Dtos;

public record GetRobotWithWeaponsDto(int Id, string CodeName, List<GetWeaponInRobotDto> Weapons);

