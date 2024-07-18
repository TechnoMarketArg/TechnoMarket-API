using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Domain.Interfaces
{
    public interface IUserRepository
    {
        User? GetByEmail(string name);
        List<User> Get();
        User AddUser(User user);
        User? DeleteUser(Guid id);
        public void Update(User user);
        public User GetById(Guid id);
    }
}
