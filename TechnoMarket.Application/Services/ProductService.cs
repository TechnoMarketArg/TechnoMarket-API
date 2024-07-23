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
        public ProductService(IProductRepository productRepository)
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

        public Product AddProduct(Product product)
        {
            return _productRepository.AddProduct(product);
        }

        public Product? DeleteProduct(Guid id)
        {
            return _productRepository.DeleteProduct(id);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
        }
    }
}