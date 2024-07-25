using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.DTOs;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Domain.Interfaces
{
    public interface IStoreRepository
    {
        public List<StoreDTO> GetStores();
        public void CreateStore(Store store, Guid userId);
        public List<StoreWithProductsDTO> GetStoreWithProducts();
        public Store GetById(Guid id);
        public void Update(Store store);
        public StoreWithProductsDTO StoreAndInventory(Guid storeId);
        public void Delete(Guid storeId);
        public bool StoreExists(Guid storeId);
    }
}