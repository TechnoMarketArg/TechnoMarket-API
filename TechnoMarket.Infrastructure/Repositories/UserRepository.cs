using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;

// Interactuar con la DB.
namespace TechnoMarket.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationContext _context; //Inyeccion de Dependencia. 
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
        public User? Get(string name)
        {
            return _context.Users.FirstOrDefault(u => u.FirstName == name);
        }
    }
}
