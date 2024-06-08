using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Application.IServices
{
    public interface IUserService
    {
        public User Get(string name);
        public List<User> Get();
        public User AddUser(User user);
        public User? DeleteUser(Guid id);
    }
}
