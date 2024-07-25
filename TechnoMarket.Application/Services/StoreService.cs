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
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public void CreateStore(Store store, Guid userId)
        {
            _storeRepository.CreateStore(store, userId);
        }

        public List<StoreDTO> GetStores()
        {
            return _storeRepository.GetStores();
        }

        public List<StoreWithProductsDTO> GetStoreWithProducts()
        {
            return _storeRepository.GetStoreWithProducts();
        }

        public Store GetById(Guid id)
        {
            Store store = _storeRepository.GetById(id);

            if (store == null)
            {
                throw new Exception("No se encontró ningún usuario con ese ID.");
            }

            return store;
        }

        public StoreWithProductsDTO StoreAndInventory(Guid storeId)
        {
            return _storeRepository.StoreAndInventory(storeId);
        }

        public void Delete(Guid storeId)
        {
            _storeRepository.Delete(storeId);
        }

        public void Update(Guid StoreId, StoreUpdateDTO storeDTO)
        {
            var store = _storeRepository.GetById(StoreId);
            if (store == null)
            {
                throw new Exception("No se encontró ningún usuario con ese ID.");
            }

            store.Name = storeDTO.Name;
            store.Description = storeDTO.Description;

            _storeRepository.Update(store);
        }
    }
}