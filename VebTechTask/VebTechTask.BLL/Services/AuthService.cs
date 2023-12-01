using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VebTech.BLL.Validation;
using VebTechTask.BLL.AutoMapper.Interfaces;
using VebTechTask.BLL.DTO;
using VebTechTask.BLL.Services.Inrefaces;
using VebTechTask.DAL.Entities;
using VebTechTask.DAL.Enums;
using VebTechTask.DAL.Repositories.Interfaces;

namespace VebTechTask.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<UserDTO> _passwordHasher;
        private readonly IUserMapper _userMapper;
        private readonly TokenService _tokenService;

        public AuthService(IAuthRepository userRepository,
            IMapper mapper,
            IPasswordHasher<UserDTO> passwordHasher,
            IUserMapper userMapper,
            TokenService tokenService)
        {
            _authRepository = userRepository
                ?? throw new ArgumentNullException();
            _mapper = mapper
                ?? throw new ArgumentNullException();
            _passwordHasher = passwordHasher
                ?? throw new ArgumentNullException();
            _userMapper = userMapper
                ?? throw new ArgumentNullException();
            _tokenService = tokenService;
        }

        public async Task<CreateUserDTO> Register(CreateUserDTO userDTO)
        {
            try
            {
                var validator = new RegisterUserValidator();
                var result = validator.Validate(userDTO);
                if (!result.IsValid)
                {
                    throw new FluentValidation.ValidationException("The entry is incorrect");
                }

                if (await _authRepository.GetUserByName(userDTO.Email) != null)
                {
                    throw new FluentValidation.ValidationException("A user with this email already exists.");
                }

                var user = new User
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Age = userDTO.Age,
                    PasswordHash = _passwordHasher.HashPassword(null, userDTO.Password)
                };

                await _authRepository.RegisterUser(user);
                return userDTO;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<string> Authenticate(string email, string password, string secretKey)
        {
            var user = await _authRepository.GetUserByName(email);
            var userDto = _userMapper.MapToDTO(user);

            if (user != null && _passwordHasher.VerifyHashedPassword(userDto, user.PasswordHash, password) == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)
            {
                var isAdmin = user.UserRoles.Any(ur => ur.Role.Name == RoleType.Admin);

                if (isAdmin)
                {
                    return _tokenService.GenerateAdminToken(email, secretKey);
                }

                return _tokenService.GenerateUserToken(email, secretKey);
            }

            return null;
        }
    }
}
