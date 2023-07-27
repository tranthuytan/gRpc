using Grpc.Net.Client;
using gRPCClient_Assignment2.Protos.Auth;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace gRPCClient_Assignment2.Controllers
{
    public class LoginController : Controller
    {
        private GrpcChannel channel;
        private Auth.AuthClient client;
        public LoginController()
        {
            this.channel = GrpcChannel.ForAddress("https://localhost:7015");
            client = new Auth.AuthClient(channel);
        }
        public IActionResult Index()
        {
            return View();
        }
        //login
        [HttpPost]
        public async Task<IActionResult> Login(protoUser login)
        {
            var reply = await client.LoginAsync(login);
            if (reply.Jwt.Contains("wrong"))
            {
                ViewData["Message"] = "Wrong username or password";
                return View("Index");
            }
            //decode jwt
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(reply.Jwt);
            var claims = token.Claims;
            var role = claims.FirstOrDefault(x=>x.Type.Contains("role"))?.Value;
            if (role != "0")
            {
                ViewData["Message"] = "Your account doesn't have permission to access this page";
                return View("Index");
            }
            //add jwt to cookies
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(30),
                Secure = true,
                HttpOnly = true,
                Path = "/"
            };
            Response.Cookies.Append("jwt", reply.Jwt, cookieOptions);
            return RedirectToAction("Index", "Book");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(protoUser register)
        {
            var isSuccess = await client.RegisterAsync(register);
            if (!isSuccess.Success)
            {
                ViewData["Message"] = "Duplicated username, or something wrong with the inputs";
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
    }
}
