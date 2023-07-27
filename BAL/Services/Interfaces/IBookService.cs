using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IBookService
    {
        public Task Add(Book book);
        public Task Update(Book book);
        public Task Delete(Book book);
        public Task<IEnumerable<Book>> GetAll();
        public Task<Book> GetById(int id);
    }
}
