using VebTechTask.BLL.DTO;

namespace VebTechTask.BLL.Services.Inrefaces
{
    public interface IAuthService
    {
        Task<CreateUserDTO> Register(CreateUserDTO userDTO);
        Task<bool> Authenticate(string name, string password);
    }
}
