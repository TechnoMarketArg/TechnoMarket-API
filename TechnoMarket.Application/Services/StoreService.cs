﻿using System;
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

        public void CreateStore(Store store)
        {
            _storeRepository.CreateStore(store);
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
    }
}