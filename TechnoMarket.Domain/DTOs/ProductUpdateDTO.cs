using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Domain.DTOs
{
    public class ProductUpdateDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool Status { get; set; } = true;

        public int Quantity { get; set; }

        public bool Offer { get; set; }

        public decimal Discount { get; set; }

    }
}