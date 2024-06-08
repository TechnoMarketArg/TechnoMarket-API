using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Application.IServices;
using TechnoMarket.Domain.Entities;
using TechnoMarket.Domain.Interfaces;

namespace TechnoMarket.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public List<Role> Get()
        {
            return _roleRepository.Get();
        }

    }
}
