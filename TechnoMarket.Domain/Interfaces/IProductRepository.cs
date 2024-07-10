using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Domain.Interfaces
{
    public interface IProductRepository
    {
        public List<Product?> GetAll();
        public Product? GetById(Guid id);
        public Product AddProduct(Product product);
    }
}
