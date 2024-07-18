using Microsoft.EntityFrameworkCore;
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

        public void CreateStore(Store store)
        {
            _context.Stores.Add(store);
            _context.SaveChanges();
        }



        public List<StoreWithProductsDTO> GetStoreWithProducts()
        {
            return _context.Stores
                .Include(s => s.Inventory)
                .Select(s => new StoreWithProductsDTO
                {
                    Name = s.Name,
                    Rating = s.Rating,
                    Inventory = s.Inventory.Select(p => new ProductDTO
                    {
                        Name = p.Name,
                        Price = p.Price
                    }).ToList()
                })
                .ToList();
        }

    }
}