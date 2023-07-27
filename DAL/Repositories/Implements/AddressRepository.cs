using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(BookStoreLab2Context context) : base(context)
        {
        }
    }
}
