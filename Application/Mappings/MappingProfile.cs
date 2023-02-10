namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Robot, GetRobotDto>();
        CreateMap<Robot, GetRobotWithWeaponsDto>();
        CreateMap<CreateUpdateRobotDto, Robot>();
    }
}
