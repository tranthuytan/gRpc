using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implements
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(BookStoreLab2Context context) : base(context)
        {
        }
    }
}
