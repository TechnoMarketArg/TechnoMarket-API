using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;
using TechnoMarket.Domain.Interfaces;

namespace TechnoMarket.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationContext _context;
        public RoleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Role> Get()
        {
            return _context.Roles.ToList();
        }
    }
}
