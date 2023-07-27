using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;
using DAL.Models;
using System.Linq.Expressions;

namespace DAL.Repositories.Implements
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(BookStoreLab2Context context) : base(context)
        {
        }
    }
}
