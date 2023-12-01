using Microsoft.EntityFrameworkCore;
using VebTechTask.DAL.Data;
using VebTechTask.DAL.Entities;
using VebTechTask.DAL.Modells;
using VebTechTask.DAL.Repositories.Interfaces;

namespace VebTechTask.DAL.Repositories
{
    public class AuthRepository  :IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context
                ?? throw new ArgumentNullException();

        }

        public async Task RegisterUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = 1
            };
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByName(string userName) =>
            await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == userName);



    }
}
