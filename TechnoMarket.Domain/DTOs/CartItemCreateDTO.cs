using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoMarket.Domain.DTOs
{
    public class CartItemCreateDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
