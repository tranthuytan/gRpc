using BAL.Services.Interfaces;
using DAL.Models;
using Grpc.Core;
using gRPCService_Assignment2.Protos;
using Microsoft.AspNetCore.Authorization;

namespace gRPCService_Assignment2.Services
{
    [Authorize]
    public class BookStoreCRUDService : BookStoreCRUD.BookStoreCRUDBase
    {
        private readonly IBookService _bookService;
        private readonly IAddressService _addressService;
        private readonly IPressService _pressService;

        public BookStoreCRUDService(IBookService bookService, IAddressService addressService, IPressService pressService)
        {
            _bookService = bookService;
            _addressService = addressService;
            _pressService = pressService;
        }

        public override async Task<Empty> Delete(protoBook request, ServerCallContext context)
        {
            var book =await _bookService.GetById(request.Id);
            if (book == null)
                return new Empty { Message = "Cannot find book with that id" };
            _bookService.Delete(book);
            return new Empty { Message = "Delete successfully"};
        }

        public override async Task<Empty> Insert(protoBook request, ServerCallContext context)
        {
            var checkDuplicate = await _bookService.GetById(request.Id);
            if (checkDuplicate != null)
                return new Empty { Message = "Duplicated Id" };
            Book addBook = new Book
            {
                Id = request.Id,
                Author = request.Author,
                Isbn = request.Isbn,
                LocationName = request.LocationName,
                Title = request.Title,
                Price = (decimal)request.Price,
                PressId = request.PressId
            };
            _bookService.Add(addBook);
            return new Empty { Message = "Added successfully" };
        }

        public override async Task<protoBooks> SelectAll(Empty request, ServerCallContext context)
        {
            var books = await _bookService.GetAll();
            var reply = new protoBooks();
            foreach (var book in books)
            {
                var press = await _pressService.GetByKey(book.PressId);
                var location = await _addressService.GetByKey(book.LocationName);
                reply.Items.Add(new protoBook
                {
                    Id = book.Id,
                    Isbn = book.Isbn,
                    Author = book.Author,
                    Title = book.Title,
                    Price = (double)book.Price,
                    LocationName = book.LocationName,
                    Location = new protoAddress
                    {
                        City = location.City,
                        Street = location.Street
                    },
                    PressId = book.PressId,
                    Press = new protoPress
                    {
                        Id = book.PressId,
                        Name = press.Name,
                        Category = press.Category
                    }
                });
            }
            return reply;
        }

        public override async Task<protoBook> SelectById(protoBookFilter request, ServerCallContext context)
        {
            var result = await _bookService.GetById(request.BookId);
            var reply = new protoBook
            {
                Id = result.Id,
                Author = result.Author,
                Isbn = result.Isbn,
                LocationName= result.LocationName,
                Title = result.Title,
                Price= (double)result.Price,
                PressId= result.PressId,
                Location = new protoAddress
                {
                    City = result.LocationName,
                    Street = result.LocationNameNavigation.Street
                },
                Press = new protoPress
                {
                    Id = result.PressId,
                    Name = result.Press.Name,
                    Category = result.Press.Category
                }
            };
            return reply;
        }

        public override async Task<Empty> Update(protoBook request, ServerCallContext context)
        {
            var checkAvailable =await _bookService.GetById(request.Id);
            if (checkAvailable == null)
                return new Empty { Message = "No book with that id" };
            checkAvailable.Isbn = request.Isbn;
            checkAvailable.PressId = request.PressId;
            checkAvailable.LocationName = request.LocationName;
            checkAvailable.Author = request.Author;
            checkAvailable.Title = request.Title;
            checkAvailable.Price = (decimal)request.Price;
            _bookService.Update(checkAvailable);
            return new Empty { Message="Update successfully"};
        }
    }
}
