using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoMarket.Domain.Entities
{
    public class Store
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Rating { get; set; } = decimal.Zero;

        public ICollection<Product>? Inventory { get; set; }

        public Store() { }
    }
}
