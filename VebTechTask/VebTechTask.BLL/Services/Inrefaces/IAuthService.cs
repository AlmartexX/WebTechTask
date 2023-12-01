using VebTechTask.BLL.DTO;

namespace VebTechTask.BLL.Services.Inrefaces
{
    public interface IAuthService
    {
        Task<CreateUserDTO> Register(CreateUserDTO userDTO);
        Task<string> Authenticate(string name, string password, string secretKey);
    }
}
