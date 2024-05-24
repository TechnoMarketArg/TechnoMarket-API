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
        User? Get(string name);
        List<User> Get();
        User AddUser(User user);
    }
}
