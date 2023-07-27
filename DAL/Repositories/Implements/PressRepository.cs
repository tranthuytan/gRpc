using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;
using DAL.Models;

namespace DAL.Repositories.Implements
{
    public class PressRepository : RepositoryBase<Press>, IPressRepository
    {
        public PressRepository(BookStoreLab2Context context) : base(context)
        {
        }
    }
}
