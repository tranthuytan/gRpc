using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IPressService
    {
        public Task<IEnumerable<Press>> GetAll();
        public Task<Press> GetByKey(object key);
    }
}
