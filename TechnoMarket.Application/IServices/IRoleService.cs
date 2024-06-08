using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Application.IServices
{
    public interface IRoleService
    {
        public List<Role> Get();
    }
}
