using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Application.IServices;
using TechnoMarket.Domain.DTOs;
using TechnoMarket.Domain.Entities;
using TechnoMarket.Domain.Interfaces;
using static System.Formats.Asn1.AsnWriter;

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

        public void UpdateProduct(ProductUpdateDTO productDTO, Guid id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "el producto no puede ser nulo.");
            }

            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.Quantity = productDTO.Quantity;
            product.Offer = productDTO.Offer;
            product.Discount = productDTO.Discount;

            _productRepository.UpdateProduct(product);
        }

        public List<CategoryDTO> GetCategories()
        {
            var categories = _productRepository.GetCategories();

            var categoryDTOs = categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            return categoryDTOs;
        }

        public List<ProductGetDTO> GetProductsByCategory(Guid categoryId)
        {
            var products = _productRepository.GetProductsByCategory(categoryId);

            var productDTOs = products.Select(product => new ProductGetDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                Offer = product.Offer,
                Discount = product.Discount,
                Status = product.Status,
                StoreId = product.StoreId,

            }).ToList();

            return productDTOs;
        }
    }
}