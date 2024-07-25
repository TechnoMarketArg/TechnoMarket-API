using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.DTOs;
using TechnoMarket.Domain.Entities;
using static System.Formats.Asn1.AsnWriter;

namespace TechnoMarket.Application.IServices
{
    public interface IStoreService
    {
        public List<StoreDTO> GetStores();
        public void CreateStore(Store store, Guid userId);
        public List<StoreWithProductsDTO> GetStoreWithProducts();
        public Store GetById(Guid id);
        public StoreWithProductsDTO StoreAndInventory(Guid storeId);
        public void Delete(Guid storeId);
        public void Update(Guid StoreId, StoreUpdateDTO storeDTO);
    }
}