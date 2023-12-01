using AutoMapper;
using VebTechTask.BLL.DTO;
using VebTechTask.BLL.DTO.Parameters;
using VebTechTask.DAL.Entities;
using VebTechTask.DAL.Enums;
using VebTechTask.DAL.Parameters;

namespace VebTechTask.BLL.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role)));
            CreateMap<Role, RoleDTO>();
            CreateMap<CreateUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();

            CreateMap<LoginUserDTO, UserDTO>();
            
            CreateMap<UserQueryParameters, UserQueryParametersDTO>();
            CreateMap<UserQueryParametersDTO, UserQueryParameters>();

        }
    }
}
