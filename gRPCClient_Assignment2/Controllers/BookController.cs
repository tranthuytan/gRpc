using Grpc.Core;
using Grpc.Net.Client;
using gRPCClient_Assignment2.Protos.Book;
using Microsoft.AspNetCore.Mvc;

namespace gRPCClient_Assignment2.Controllers
{
    public class BookController : Controller
    {
        private GrpcChannel channel;
        private BookStoreCRUD.BookStoreCRUDClient client;
        private string jwtToken;
        private Metadata headers;
        private CallOptions options;
        public BookController()
        {
            this.channel = GrpcChannel.ForAddress("https://localhost:7015");
            client = new BookStoreCRUD.BookStoreCRUDClient(channel);
        }
        private void GetJWT()
        {
            //get JWT
            jwtToken = HttpContext.Request.Cookies["jwt"];
            headers = new Metadata
            {
                {"Authorization", $"Bearer {jwtToken}" }
            };
            options = new CallOptions(headers: headers);
        }
        
        public async Task<IActionResult> Index()
        {
            GetJWT();
            if (jwtToken == null)
                return RedirectToAction("Index", "Login");
            var reply = await client.SelectAllAsync(new Empty { Message = "" },options);
            return View(reply);
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(protoBook book)
        {
            GetJWT();
            if (jwtToken == null)
                return RedirectToAction("Index", "Login");
            var reply = await client.InsertAsync(book,options);
            if (reply.Message.Contains("Duplicated"))
            {

                return View();
            }
            return RedirectToAction("Index");
        }

        //Delete
        public async Task<IActionResult> Delete(int id)
        {
            GetJWT();
            if (jwtToken == null)
                return RedirectToAction("Index", "Login");
            var book = await client.SelectByIdAsync(new protoBookFilter { BookId = id },options);
            return View(book);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int? unuseVar = null)
        {
            GetJWT();
            if (jwtToken == null)
                return RedirectToAction("Index", "Login");
            var book = await client.SelectByIdAsync(new protoBookFilter { BookId = id },options);
            var reply = await client.DeleteAsync(book,options);
            if (!reply.Message.Contains("successfully"))
            {
                return View();
            }
            return RedirectToAction("Index");
        }
        //Details
        public async Task<IActionResult> Details(int id)
        {
            GetJWT();
            if (jwtToken == null)
                return RedirectToAction("Index", "Login");
            var book = await client.SelectByIdAsync(new protoBookFilter { BookId = id },options);
            return View(book);
        }
        //Edit
        public async Task<IActionResult> Edit(int id)
        {
            GetJWT();
            if (jwtToken == null)
                return RedirectToAction("Index", "Login");
            var book = await client.SelectByIdAsync(new protoBookFilter { BookId = id },options);
            return View(book);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(protoBook pBook, int? unuseVar = null)
        {
            GetJWT();
            if (jwtToken == null)
                return RedirectToAction("Index", "Login");
            var book = await client.SelectByIdAsync(new protoBookFilter { BookId = pBook.Id },options);
            book = pBook;
            var reply = await client.UpdateAsync(book, options);
            if (!reply.Message.Contains("successfully"))
            {
                
                return View(pBook);
            }
            return RedirectToAction("Index");
        }
    }
}
