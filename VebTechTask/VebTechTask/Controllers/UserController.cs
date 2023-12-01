using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VebTechTask.BLL.DTO;
using VebTechTask.BLL.DTO.Parameters;
using VebTechTask.BLL.Services.Inrefaces;

namespace VebTechTask.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService service)
        {
            _userService = service
                     ?? throw new ArgumentNullException();
        }

        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        [ProducesResponseType(typeof(List<UserDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(
         Summary = "Get a list of users with pagination, sorting, and filtering options.",
         Description = "Retrieve a list of users with options to paginate, sort, and filter the results.")]
        public async Task<IActionResult> GetUsersAsync([FromQuery] UserQueryParametersDTO queryParameters)
        {
            var users = await _userService.GetAllUsersAsync(queryParameters);
            if (users == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No users in database.");
            }

            return StatusCode(StatusCodes.Status200OK, users);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<UserDTO>> GetUserByIdAsync(int? id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"No users found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, user);
        }

        [HttpPost("{userId}/roles")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AddRoleToUser(int userId, [FromBody] string role)
        {
            var user = _userService.AddRoleToUserAsync(userId, role);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"Role could not be added.");
            }

            return StatusCode(StatusCodes.Status200OK, $"A new role has been added to user");
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> UpdateUserAsync(UpdateUserDTO updateUser, int? id)
        {
            var user = await _userService.UpdateUserAsync(updateUser, id);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"{updateUser.Name} could not be updated");
            }

            return StatusCode(StatusCodes.Status200OK, $"{updateUser.Name} successfully changed");
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> DeleteUserAsync(int? id)
        {
            (bool status, string message) = await _userService.DeleteUserAsync(id);
            if (status == false)
            {
                return StatusCode(StatusCodes.Status404NotFound, message);
            }

            return StatusCode(StatusCodes.Status200OK, $"{id.Value} successfully deleted");
        }
    }

}
