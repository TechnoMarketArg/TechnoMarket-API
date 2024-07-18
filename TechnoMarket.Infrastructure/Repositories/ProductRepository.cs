using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;
using TechnoMarket.Domain.Interfaces;

namespace TechnoMarket.Infrastructure.Repositories
{

    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;
        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
           return _context.Products.ToList();
        }

        public Product? GetById(Guid id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            return product;
        }

        public Product? DeleteProduct(Guid id)
        {
            var product = _context.Products.FirstOrDefault(p =>p.Id == id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return product;
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

    }
}
