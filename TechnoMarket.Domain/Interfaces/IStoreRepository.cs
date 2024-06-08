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
        public List<Store> GetStores();
        public List<StoreWithProductsDTO> GetStoreWithProducts();
    }
}
