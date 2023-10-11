using AutoMapper;
using VebTechTask.BLL.AutoMapper.Interfaces;
using VebTechTask.BLL.DTO;
using VebTechTask.BLL.DTO.Parameters;
using VebTechTask.DAL.Entities;
using VebTechTask.DAL.Parameters;

namespace VebTechTask.BLL.AutoMapper
{
    public class UserMapper : IUserMapper
    {
        private readonly IMapper _mapper;

        public UserMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UserDTO MapToDTO(User user)
        {
            return _mapper.Map<UserDTO>(user);
        } 
        
        public UserQueryParameters MapParamToDTO(UserQueryParametersDTO paramDto)
        {
            return _mapper.Map<UserQueryParameters>(paramDto);
        }

        public User MapToEntity(CreateUserDTO newUserDto)
        {
            return _mapper.Map<User>(newUserDto);
        }

        public void MapToEntity(UpdateUserDTO userDTO, User existingUser)
        {
            _mapper.Map(userDTO, existingUser);
        }
    }
}
