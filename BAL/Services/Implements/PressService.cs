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
    public class PressService : IPressService
    {
        private readonly IPressRepository _pressRepository;

        public PressService(IPressRepository pressRepository)
        {
            _pressRepository = pressRepository;
        }
        public async Task<IEnumerable<Press>> GetAll()
        {
            return await _pressRepository.GetAll();
        }

        public async Task<Press> GetByKey(object key)
        {
            return await _pressRepository.GetById(key);
        }
    }
}
