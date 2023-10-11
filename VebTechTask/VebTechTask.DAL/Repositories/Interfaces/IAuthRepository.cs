using VebTechTask.DAL.Entities;

namespace VebTechTask.DAL.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task RegisterUser(User user);
        Task<User> GetUserByName(string userName);
    }
}
