namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Robot, GetRobotDto>();
        CreateMap<Robot, GetRobotWithWeaponsDto>();
        CreateMap<Weapon, GetWeaponDto>();
        
        CreateMap<Weapon, GetWeaponInRobotDto>();

        CreateMap<CreateUpdateRobotDto, Robot>();
    }
}
