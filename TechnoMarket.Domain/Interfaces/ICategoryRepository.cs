using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        public List<Category> GetCategories();
        public bool CategoryExists(Guid categoryId);
        public Category GetCategoryById(Guid categoryId);
        public void Add(Category category);
    }
}
