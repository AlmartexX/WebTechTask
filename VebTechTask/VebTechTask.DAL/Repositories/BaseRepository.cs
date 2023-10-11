using Microsoft.EntityFrameworkCore;
using VebTechTask.DAL.Data;
using VebTechTask.DAL.Repositories.Interfaces;

namespace VebTechTask.DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context
                ?? throw new ArgumentNullException();

        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>()
                .Include("UserRole.Role")
                .AsNoTracking()
                .ToListAsync();

        }

        public virtual async Task<TEntity> Get1ByIdAsync(int id) =>
            await _context.Set<TEntity>()
                .FindAsync(id);

        public virtual async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>()
                .AddAsync(entity);
            await _context.SaveChangesAsync();

        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>()
                .Update(entity);
            await _context.SaveChangesAsync();

        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<TEntity>()
                .FindAsync(id);

            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }

        }
    }
}
