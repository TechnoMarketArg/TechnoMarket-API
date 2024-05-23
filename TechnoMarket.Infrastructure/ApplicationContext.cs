using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
