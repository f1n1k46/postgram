using Postgram.DTOs.AuthDto;
using Postgram.Helpers;
using Postgram.Repositories;
using Postgram.DTOs.User;
using FluentValidation;

namespace Postgram.Services
{
    public interface IAuthService {
        public Task<AuthResponseDto> LoginAsync(LoginDto dto);
        public Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    }
    
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<RegisterDto> _registerValidator;

        public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator, IValidator<RegisterDto> registerValidator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
            _registerValidator = registerValidator;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetUserByEmailAsync(dto.Email);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password.");

            if (!_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password.");

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var result = await _registerValidator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(dto.Email);

            if (existingUser != null)
                throw new ArgumentException("Email is already in use.");

            var hashedPassword = _passwordHasher.HashPassword(dto.Password);

            var newUser = await _userRepository.CreateUserAsync(new CreateUserDto
            {
                Name = dto.Name,
                Username = dto.Username,
                Nickname = dto.Nickname,
                Age = dto.Age,
                Email = dto.Email,
                Password = hashedPassword
            });

            var token = _jwtTokenGenerator.GenerateToken(newUser);

            return new AuthResponseDto
            {
                Token = token
            };
        }
    }
}
