using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoMarket.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public ICollection<Product> Products { get; set; }
        public Buyer Buyer { get; set; }
    }

}