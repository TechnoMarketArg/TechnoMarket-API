using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.DTOs;
using TechnoMarket.Domain.Entities;
using TechnoMarket.Domain.Interfaces;

namespace TechnoMarket.Infrastructure.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationContext _context;
        public StoreRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Store> GetStores()
        {
            return _context.Stores.ToList();
        }

        public List<StoreWithProductsDTO> GetStoreWithProducts() 
        {
            return _context.Stores
                    .Join(
                        _context.Products,
                        s => s.Id,
                        p => p.Store.Id,
                        (s, p) => new { Store = s, Product = p }
                    )
                    .GroupBy(
                        sp => new { sp.Store.Id, sp.Store.Name, sp.Store.Rating },
                        sp => sp.Product,
                        (key, products) => new StoreWithProductsDTO
                        {
                            Name = key.Name,
                            Rating = key.Rating,
                            Inventory = products.Select(c => new ProductDTO
                            {
                                Name = c.Name,
                                Price = c.Price

                            }).ToList()
                        }
                    )
                    .ToList();
        }
    }
}