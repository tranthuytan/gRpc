using BAL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _addressRepository.GetAll();
        }

        public async Task<Address> GetByKey(object key)
        {
            return await _addressRepository.GetById(key);
        }
    }
}
