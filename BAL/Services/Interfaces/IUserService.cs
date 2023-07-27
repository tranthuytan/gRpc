using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BAL.Services.Interfaces
{
    public interface IUserService
    {
        public Task<string> Login(User user);
        public Task<string> Register(User user);
    }
}
