using Grpc.Net.Client;
using gRPCClient_Assignment2.Models;
using gRPCClient_Assignment2.Protos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace gRPCClient_Assignment2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private GrpcChannel channel; 

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            channel = GrpcChannel.ForAddress("https://localhost:7015");
        }

        public async Task<IActionResult> IndexAsync()
        {
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "Hello, hehe" });
            return View(reply);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}