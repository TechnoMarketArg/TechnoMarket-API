using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Domain.DTOs
{
    public class StoreWithProductsDTO
    {
        public string Name { get; set; }

        public decimal Rating { get; set; }

        public List<ProductDTO> Inventory { get; set; }
    }



}