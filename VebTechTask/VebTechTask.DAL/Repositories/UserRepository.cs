using Microsoft.EntityFrameworkCore;
using System.Data;
using VebTechTask.DAL.Data;
using VebTechTask.DAL.Entities;
using VebTechTask.DAL.Modells;
using VebTechTask.DAL.Parameters;
using VebTechTask.DAL.Repositories.Interfaces;

namespace VebTechTask.DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context
                   ?? throw new ArgumentNullException();

        }

        public async Task<List<User>> GetAllUsersWithRolesAsync()
        {
            var usersWithRoles = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .AsNoTracking()
                .ToListAsync();

            return usersWithRoles;
        }

        public async Task<User> GetByIdAsync(int id) =>
            await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<List<User>> GetUsersAsync(UserQueryParameters arg)
        {
            var query = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(arg.Filter))
            {
                    query = query.Where(u =>
                    u.Name.Contains(arg.Filter) ||
                    u.Email.Contains(arg.Filter) ||
                    u.Age.ToString().Contains(arg.Filter) ||
                    u.UserRoles.Any(r => r.RoleId.ToString() == arg.Filter));

            }

            if (arg.Ascending)
            {
                query = query.OrderBy(u => EF.Property<object>(u, arg.SortField));
            }
            else
            {
                query = query.OrderByDescending(u => EF.Property<object>(u, arg.SortField));
            }

            var users = await query
                .Skip((arg.Page - 1) * arg.PageSize)
                .Take(arg.PageSize)
                .ToListAsync();

            return users;
        }

        public async Task AddRoleToUserAsync(int userId, string role)
        {
            int number;
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (int.TryParse(role, out number))
            {
                var userRole = new UserRole
                {
                    UserId = userId,
                    RoleId = number
                };
                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUserWithRoleAsync(User user)
        {
            _context.Users
             .Update(user);
            await _context.SaveChangesAsync();
           
        }
    }

}
