using DDD.Application.Common.Errors;
using DDD.Application.Common.Interfaces.Authentication;
using DDD.Application.Common.Interfaces.Persistence;
using DDD.Domain.Entities;

namespace DDD.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            // Validate the user doesn't exist
            if (_userRepository.GetUserEmail(email) is not null)
            {
                throw new DuplicateEmailException();
            }

            // Create user (generate unique id) & persist to db
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _userRepository.Add(user);

            // Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }

        public AuthenticationResult Login(string email, string password)
        {
            // Validate the user exists
            if (_userRepository.GetUserEmail(email) is not User user)
            {
                throw new Exception("User with given email doesn't exists");
            }

            // Validate the password is correct
            if (user.Password != password)
            {
                throw new Exception("Invalid password");
            }

            // Create Jwt Token 
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token
                );



        }
    }
}
