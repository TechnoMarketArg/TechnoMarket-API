using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Application.IServices;
using TechnoMarket.Domain.Entities;
using TechnoMarket.Domain.Interfaces;

namespace TechnoMarket.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService (IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product?> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product? GetById(Guid id)
        {
            return _productRepository.GetById(id);
        }
    }
}
