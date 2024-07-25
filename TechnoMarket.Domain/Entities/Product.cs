using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoMarket.Domain.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool Status { get; set; } = true;

        public decimal Rating { get; set; } = 0;

        public int Quantity { get; set; }

        public bool Offer { get; set; } = false;

        public decimal Discount { get; set; } = 0;

        public Guid StoreId {  get; set; }

        public Store Store { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        public Product() { }

    }
}