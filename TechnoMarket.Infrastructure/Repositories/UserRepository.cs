using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;
using TechnoMarket.Domain.Interfaces;

// Interactuar con la DB.
namespace TechnoMarket.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
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

        public List<User> Get()
        {
            return _context.Users.ToList();
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User DeleteUser(int id) 
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == id);

            _context.Users.Remove(user);
            _context.SaveChanges();

            return user;
        }
    }
}
