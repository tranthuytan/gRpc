using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IAddressService
    {
        public Task<IEnumerable<Address>> GetAll();
        public Task<Address> GetByKey(object key);  
    }
}
