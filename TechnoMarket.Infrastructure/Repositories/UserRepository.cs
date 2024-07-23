using Microsoft.EntityFrameworkCore;
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

        public User? GetByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
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

        public User? DeleteUser(Guid id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return user;
        }

        public void Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El usuario no puede ser nulo.");
            }

            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Error al actualizar el usuario en la base de datos.", ex);
            }
        }


        public User GetById(Guid id)
        {
            var user = _context.Users
                .Include(u => u.Store)
                .FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("Usuario no encontrado");
            }

        }

        public void ChangeActive(Guid id)
        {
            var user = GetById(id);

            if (user != null)
            {
                user.Active = !user.Active;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Usuario no encontrado");
            }
        }
    }
}
