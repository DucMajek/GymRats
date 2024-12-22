using gymrats.server.Models;
using gymrats.server.Models.DTOs;
using gymrats.server.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Protocol.Plugins;

namespace gymrats.server.Services
{
    public interface IUserServices
    {
        Task<LoginResponseDto> GetUser(LoginRequestDto login);
        Task<RegisterUserResponseDto> RegisterUser(RegisterUserRequestDto newUser);
    }
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;
        public UserServices(IUserRepository userRepository, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<LoginResponseDto> GetUser(LoginRequestDto login)
        {

            var user = await _userRepository.UserExist(login);

            if (user)
            {
                var token = _tokenGenerator.GenerateToken(login.Email);
                return new LoginResponseDto { 
                    Token = token,
                    Message = "Login successful"
                };
            }
            return new LoginResponseDto
            {
                Token = null,
                Message = "Wrong username or password"
            };

        }

        public async Task<RegisterUserResponseDto> RegisterUser(RegisterUserRequestDto newUser)
        {
            try
            {
                var user = await _userRepository.EmailExist(newUser);
                if (user)
                {
                    return new RegisterUserResponseDto
                    {
                        Message = "Email already exists",
                        Status = "Error"
                    };
                }

                var registerUser = await _userRepository.AddNewUser(newUser);

                return new RegisterUserResponseDto
                {
                    Message = "Registration successful!",
                    Status = "Success"
                };
            }
            catch (Exception ex)
            {
                return new RegisterUserResponseDto
                {
                    Message = "An error occurred during registration: " + ex.Message
                };
            }
        }


    }
}
