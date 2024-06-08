using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Domain.Interfaces
{
    public interface IRoleRepository
    {
        public List<Role> Get();
    }
}
