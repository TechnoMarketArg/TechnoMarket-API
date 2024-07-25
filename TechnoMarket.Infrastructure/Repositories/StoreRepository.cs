using Microsoft.AspNet.Identity;
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

        public List<StoreDTO> GetStores()
        {
            return _context.Stores
        .Include(s => s.Owner)
        .Select(s => new StoreDTO
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description,
            Owner = new OwnerDTO
            {
                Id = s.Owner.Id,
                Email = s.Owner.Email,
                FirstName = s.Owner.FirstName,
                LastName = s.Owner.LastName
            }
        }).ToList();
        }

        public void CreateStore(Store store, Guid userId)
        {
            _context.Stores.Add(store);
            _context.SaveChanges();

            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.StoreId = store.Id;
                user.Role = UserRole.Seller;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
        }



        public List<StoreWithProductsDTO> GetStoreWithProducts()
        {
            return _context.Stores
                .Include(s => s.Inventory)
                .Select(s => new StoreWithProductsDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Rating = s.Rating,
                    Inventory = s.Inventory.Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description,
                        Quantity = p.Quantity,
                    }).ToList()
                })
                .ToList();
        }

        public Store GetById(Guid id)
        {
            var store = _context.Stores
                .FirstOrDefault(u => u.Id == id);

            if (store != null)
            {
                return store;
            }
            else
            {
                throw new Exception("Store no encontrada");
            }

        }

        public void Update(Store store)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store), "La store no puede ser nula.");
            }

            try
            {
                _context.Stores.Update(store);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Error al actualizar el usuario en la base de datos.", ex);
            }
        }

        public StoreWithProductsDTO StoreAndInventory(Guid storeId)
        {
            var storeAndInventory = _context.Stores
            .Include(s => s.Inventory)
            .Where(s => s.Id == storeId)
            .Select(s => new StoreWithProductsDTO
            {
                Id = s.Id,
                Name = s.Name,
                Rating = s.Rating,
                Inventory = s.Inventory.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    Quantity = p.Quantity,
                }).ToList()
            }).FirstOrDefault();

            return storeAndInventory;
        }

        public void Delete(Guid storeId)
        {
            var store = _context.Stores.FirstOrDefault(s => s.Id == storeId);
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store), "la tienda no puede ser nula.");
            }

            _context.Stores.Remove(store);
            _context.SaveChanges();
        }

    }
}