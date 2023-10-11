using VebTechTask.DAL.Entities;
using VebTechTask.DAL.Parameters;

namespace VebTechTask.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<List<User>> GetAllUsersWithRolesAsync();
        public Task<User> GetByIdAsync(int id);
        public Task AddRoleToUserAsync(int userId, string role);
        public Task<List<User>> GetUsersAsync(UserQueryParameters arg);
        public Task UpdateUserWithRoleAsync(User user);

    }
}
