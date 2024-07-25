using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.DTOs;
using TechnoMarket.Domain.Entities;
using TechnoMarket.Domain.Interfaces;

namespace TechnoMarket.Application.IServices
{
    public interface IProductService
    {
        public List<Product?> GetAll();

        public Product? GetById(Guid id);

        public void AddProduct(Product product);

        public void DeleteProduct(Guid id);
        public void UpdateProduct(ProductUpdateDTO productDTO, Guid id);
        public List<CategoryDTO> GetCategories();
        public List<ProductGetDTO> GetProductsByCategory(Guid categoryId);
        public void AddCategory(CategoryCreateDTO categoryDTO);

    }
}