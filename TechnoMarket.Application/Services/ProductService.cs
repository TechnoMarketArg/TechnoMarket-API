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
        private readonly IStoreRepository _storeRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductService(IProductRepository productRepository, IStoreRepository storeRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _storeRepository = storeRepository;
            _categoryRepository = categoryRepository;
        }

        public List<Product?> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product? GetById(Guid id)
        {
            return _productRepository.GetById(id);
        }

        public void AddProduct(Product product)
        {
            var storeExists = _storeRepository.StoreExists(product.StoreId);
            if (!storeExists)
            {
                throw new Exception("Store does not exist.");
            }

            /*var categoryExists = _categoryRepository.CategoryExists(product.CategoryId);
            if (!categoryExists)
            {
                throw new Exception("Category does not exist.");
            }*/

            _productRepository.AddProduct(product);
        }

        public void DeleteProduct(Guid id)
        {
            _productRepository.DeleteProduct(id);
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
            var categories = _categoryRepository.GetCategories();

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
                Category = new CategoryDTO
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name
                }
            }).ToList();

            return productDTOs;
        }

        public void AddCategory(CategoryCreateDTO categoryDTO)
        {
            Category newCategory  = new Category()
            {
                Name = categoryDTO.Name,
                Description= categoryDTO.Description,
            };

            _categoryRepository.Add(newCategory);
        }

    }
}