using BAL.Services.Interfaces;
using DAL.Models;
using Grpc.Core;
using gRPCService_Assignment2.Protos;

namespace gRPCService_Assignment2.Services
{
    public class AuthService : Auth.AuthBase
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }
        public override async Task<protoJwt> Login(protoUser request, ServerCallContext context)
        {
            protoJwt jwtToken = new protoJwt { Jwt = "wrong" };
            User login = new User
            {
                Id = request.Id,
                Name = request.Name,
                Username = request.Username,
                Password = request.Password,
                Role = request.Role
            };
            string token = await _userService.Login(login);
            if (token.Contains("Wrong"))
                return jwtToken;
            jwtToken = new protoJwt { Jwt = token };
            return jwtToken;
        }

        public override async Task<isSuccess> Register(protoUser request, ServerCallContext context)
        {
            User register = new User
            {
                Id = request.Id,
                Name = request.Name,
                Username = request.Username,
                Password = request.Password,
                Role = request.Role
            };
            string result = await _userService.Register(register);
            if (result.Contains("successfully"))
            {
                return new isSuccess { Success = true };
            }
            return new isSuccess { Success = false };
        }
    }
}
