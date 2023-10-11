﻿using VebTechTask.DAL.Entities;

namespace VebTechTask.BLL.DTO
{
    public class UserDTO
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<RoleDTO> Roles { get; set; }
    }
}
