using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Application.Services
{
    public class UserService
    {
        public User Get(string name) 
        { 
            return new User() { FirstName = name };
        }
    }
}
