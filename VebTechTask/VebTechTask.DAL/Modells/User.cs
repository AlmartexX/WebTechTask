﻿using VebTechTask.DAL.Modells;

namespace VebTechTask.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
