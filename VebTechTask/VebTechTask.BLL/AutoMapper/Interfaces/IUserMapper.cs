using VebTechTask.BLL.DTO;
using VebTechTask.BLL.DTO.Parameters;
using VebTechTask.DAL.Entities;
using VebTechTask.DAL.Parameters;

namespace VebTechTask.BLL.AutoMapper.Interfaces
{
    public interface IUserMapper
    {
        UserDTO MapToDTO(User user);
        User MapToEntity(CreateUserDTO newUserDto);
        UserQueryParameters MapParamToDTO(UserQueryParametersDTO paramDto);
        void MapToEntity(UpdateUserDTO userDTO, User existingUser);
    }
}
