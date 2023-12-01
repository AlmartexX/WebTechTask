using VebTechTask.DAL.Entities;

namespace VebTechTask.BLL.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public List<RoleDTO> Roles { get; set; }
    }
}
