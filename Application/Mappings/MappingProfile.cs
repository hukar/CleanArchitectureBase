using Application.Dtos;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Robot, GetRobotDto>();
    }
}
