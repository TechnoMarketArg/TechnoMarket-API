using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;
using TechnoMarket.Domain.Interfaces;

namespace TechnoMarket.Application.IServices
{
    public interface IProductService
    {
        public List<Product?> GetAll();

        public Product? GetById(Guid id);

        public Product AddProduct(Product product);

    }
}
