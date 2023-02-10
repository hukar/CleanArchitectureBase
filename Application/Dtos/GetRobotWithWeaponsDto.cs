namespace Application.Dtos;

public record GetRobotWithWeaponsDto(int Id, string CodeName, List<Weapon> Weapons);

