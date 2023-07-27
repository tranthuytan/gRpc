using BAL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class BookService : IBookService
    {
        private IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task Add(Book book)
        {
            await _bookRepository.Add(book);
        }

        public async Task Delete(Book book)
        {
            await _bookRepository.Delete(book);
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _bookRepository.GetAll(null,query=>query.Include(x=>x.LocationNameNavigation).Include(x=>x.Press));
        }

        public async Task<Book> GetById(int id)
        {
            return await _bookRepository.FirstOrDefault(x => x.Id == id, query => query.Include(x => x.LocationNameNavigation).Include(x => x.Press));
        }

        public async Task Update(Book book)
        {
            await _bookRepository.Update(book);
        }
    }
}
