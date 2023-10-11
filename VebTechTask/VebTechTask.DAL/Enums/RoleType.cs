using System.ComponentModel.DataAnnotations;

namespace VebTechTask.DAL.Enums
{
    public enum RoleType
    {
        [Display(Name = "User")]
        User,
        [Display(Name = "Admin")]
        Admin,
        [Display(Name = "Support")]
        Support,
        [Display(Name = "SuperAdmin")]
        SuperAdmin,

    }
}
