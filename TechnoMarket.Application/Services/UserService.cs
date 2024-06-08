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
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public User Get(string name) 
        { 
            return _repository.Get(name);
        }

        public List<User> Get()
        {
            return _repository.Get();
        }

        public User AddUser(User user)
        {
            return _repository.AddUser(user);
        }

        public User? DeleteUser(Guid id)
        {
            return _repository.DeleteUser(id);
        }
    }
}
