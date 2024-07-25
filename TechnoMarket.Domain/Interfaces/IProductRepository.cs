using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.DTOs;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Domain.Interfaces
{
    public interface IProductRepository
    {
        public List<Product?> GetAll();
        public Product? GetById(Guid id);
        public Product AddProduct(Product product);
        public Product? DeleteProduct(Guid id);
        public void UpdateProduct(Product product);
        public List<Category> GetCategories();
        public List<Product> GetProductsByCategory(Guid categoryId);
    }
}