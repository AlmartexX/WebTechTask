using VebTechTask.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using VebTechTask.DAL.Modells;
using VebTechTask.DAL.Enums;

namespace VebTechTask.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<User>().HasData(
               new User[]
               {
                    new User { Id=1, Name="user1", Age = 20, Email="user1@gmail.com", PasswordHash="12345678"},
                    new User { Id=2, Name="user2", Age = 21, Email="user2@gmail.com", PasswordHash="87654321"},
                    new User { Id=3, Name="user3", Age = 24, Email="user3@gmail.com", PasswordHash="123123123"},
                    new User { Id=4, Name="user4", Age = 22, Email="user4@gmail.com", PasswordHash="321321321"},
                    new User { Id=5, Name="user5", Age = 26, Email="user5@gmail.com", PasswordHash="432123332"},
                    
               });

            modelBuilder.Entity<Role>().HasData(
               new Role[]
               {
                    new Role { Id=1, Name=RoleType.User},
                    new Role { Id=2, Name=RoleType.Admin},
                    new Role { Id=3, Name=RoleType.Support},
                    new Role { Id=4, Name=RoleType.SuperAdmin},
               });

            modelBuilder.Entity<UserRole>().HasData(
               new UserRole[]
               {
                    new UserRole { UserId = 1, RoleId =1 },
                    new UserRole { UserId = 2, RoleId =1 },
                    new UserRole { UserId = 3, RoleId =1 },
                    new UserRole { UserId = 4, RoleId =1 },
                    new UserRole { UserId = 5, RoleId =1 },
                    new UserRole { UserId = 1, RoleId =2 },
                 
               });
        }
    }
}
