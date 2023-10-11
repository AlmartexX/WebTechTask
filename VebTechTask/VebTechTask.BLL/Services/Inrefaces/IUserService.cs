using VebTechTask.BLL.DTO;
using VebTechTask.BLL.DTO.Parameters;

namespace VebTechTask.BLL.Services.Inrefaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(int? id);
        Task<List<UserDTO>> GetAllUsersAsync(UserQueryParametersDTO queryParameters);
        Task AddRoleToUserAsync(int userId, string role);
        Task<UpdateUserDTO> UpdateUserAsync(UpdateUserDTO userDTO, int? id);
        Task<(bool, string)> DeleteUserAsync(int? id);
    }
}
