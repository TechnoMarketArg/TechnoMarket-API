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

        public decimal Price { get; set; }

        public Store Store {  get; set; }

        //public int StoreId {  get; set; }

        public Product() { }

    }
}
