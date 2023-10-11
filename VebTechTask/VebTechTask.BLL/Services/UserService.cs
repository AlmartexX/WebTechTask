using Microsoft.Extensions.Logging;
using VebTechTask.BLL.AutoMapper.Interfaces;
using VebTechTask.BLL.DTO;
using VebTechTask.BLL.DTO.Parameters;
using VebTechTask.BLL.Services.Inrefaces;
using VebTechTask.BLL.Validation.Interfaces;
using VebTechTask.DAL.Repositories.Interfaces;
using static VebTechTask.BLL.Validation.ValidationException;

namespace VebTechTask.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IValidationPipelineBehavior<CreateUserDTO, CreateUserDTO> _createUserValidator;
        private readonly IValidationPipelineBehavior<UpdateUserDTO, UpdateUserDTO> _updateUserValidator;
        private readonly IUserMapper _userMapper;


        public UserService(
          IUserRepository userRepository,
          ILogger<UserService> logger,
          IValidationPipelineBehavior<CreateUserDTO, CreateUserDTO> createUserValidator,
          IValidationPipelineBehavior<UpdateUserDTO, UpdateUserDTO> updateUserValidator,
          IUserMapper userMapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _createUserValidator = createUserValidator ?? throw new ArgumentNullException(nameof(createUserValidator));
            _updateUserValidator = updateUserValidator ?? throw new ArgumentNullException(nameof(updateUserValidator));
            _userMapper = userMapper;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync(UserQueryParametersDTO queryParameters)
        {
            var userQueryParameters = _userMapper.MapParamToDTO(queryParameters);
            var users = await _userRepository.GetUsersAsync(userQueryParameters);
            var userDTOs = users.Select(user => _userMapper.MapToDTO(user)).ToList();

            return userDTOs;
        }

        public async Task<UserDTO> GetUserByIdAsync(int? id)
        {
            var book = await _userRepository.GetByIdAsync(id.Value);
            if (book == null)
            {
                throw new NotFoundException("No records with this id in database");
            }

            return _userMapper.MapToDTO(book);
        }

        public async Task AddRoleToUserAsync(int userId, string role)
        {
                await _userRepository.AddRoleToUserAsync(userId, role);
        }

        public async Task<UpdateUserDTO> UpdateUserAsync(UpdateUserDTO UserDTO, int? id)
        {
            _logger.LogInformation("--> User started update process!");

            return await _updateUserValidator.Process(UserDTO, async () =>
            {
                var existingUser = await _userRepository.GetByIdAsync(id.Value);
                _userMapper.MapToEntity(UserDTO, existingUser);
                await _userRepository.UpdateUserWithRoleAsync(existingUser);
                _logger.LogInformation("--> User updated!");
                
                return UserDTO;
            });

        }

        public async Task<(bool, string)> DeleteUserAsync(int? id)
        {
            _logger.LogInformation("--> Users started delete process!");
            var user = await _userRepository.GetByIdAsync(id.Value);
            if (user == null)
            {
                throw new NotFoundException("No records with this id in database");
            }
            await _userRepository.DeleteAsync(id.Value);
            _logger.LogInformation("--> User deleted!");

            return (true, "User got deleted.");
        }
    }
}
