namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request.Estate.Outlet;
    using Application.DTO.Request.Estate.Warehouse;
    using Application.DTO.Response.Estate;
    using Application.Interfaces;
    using Domain.Models;
    using Domain.Repository;

    public class EstateService : IEstateService
    {
        private IOutletRepository _outletRepository;
        private IWarehouseRepository _warehouseRepository;
        private IOutletProductRepository _outletProductRepository;
        private IWarehouseProductRepository _warehouseProductRepository;

        public EstateService(
            IOutletRepository outletRepositor,
            IWarehouseRepository warehouseRepository,
            IOutletProductRepository outletProductRepository,
            IWarehouseProductRepository warehouseProductRepository)
        {
            _outletRepository = outletRepositor;
            _warehouseRepository = warehouseRepository;
            _outletProductRepository = outletProductRepository;
            _warehouseProductRepository = warehouseProductRepository;
        }

        public List<OutletDto> GetOutlets(int warehouseId)
        {
            return _outletRepository.GetItems().Where(o => o.CityId == warehouseId).Select(o => new OutletDto(o)).ToList();
        }

        public List<WarehouseDto> GetWarehousesByCity(int cityId)
        {
            return _warehouseRepository.GetItems().Where(o => o.CityId == cityId).Select(o => new WarehouseDto(o)).ToList();
        }

        public List<WarehouseDto> GetWarehouses(int regionId)
        {
            return _warehouseRepository.GetItems().Where(o => o.City.RegionId == regionId).Select(o => new WarehouseDto(o)).ToList();
        }

        public OutletDto CreateOutlet(OutletCreateRequestDto outlet)
        {
            var outletModel = _outletRepository.CreateItem(outlet.ToModel());
            return new OutletDto(outletModel);
        }

        public WarehouseDto CreateWarehouse(WarehouseCreateRequestDto warehouse)
        {
            var warehouseModel = _warehouseRepository.CreateItem(warehouse.ToModel());
            return new WarehouseDto(warehouseModel);
        }

        public OutletDto UpdateOutlet(OutletUpdateRequestDto outlet)
        {
            var outletModel = _outletRepository.GetItem(outlet.Id);
            if (outletModel == null)
            {
                outletModel = _outletRepository.UpdateItem(outlet.ToModel());
            }

            return new OutletDto(outletModel);
        }

        public WarehouseDto UpdateWarehouse(WarehouseUpdateRequestDto warehouse)
        {
            var warehouseModel = _warehouseRepository.GetItem(warehouse.Id);
            if (warehouseModel == null)
            {
                warehouseModel = _warehouseRepository.UpdateItem(warehouse.ToModel());
            }

            return new WarehouseDto(warehouseModel);
        }

        public int DeleteOutlet(int outletId)
        {
            var outletModel = _outletRepository.GetItem(outletId);
            return outletModel != null ? _outletRepository.DeleteItem(outletModel) : 0;
        }

        public int DeleteWarehouse(int warehouseId)
        {
            var warehouseModel = _warehouseRepository.GetItem(warehouseId);
            return warehouseModel != null ? _warehouseRepository.DeleteItem(warehouseModel) : 0;
        }

        public int SetProductCount(Guid productId, int unitId, bool outlet, int count)
        {
            return SetOrAdd(productId, unitId, count, outlet, false);
        }

        public int AddProductCount(Guid productId, int unitId, bool outlet, int count)
        {
            return SetOrAdd(productId, unitId, count, outlet, true);
        }

        private int SetOrAdd(Guid productId, int unitId, int count, bool outlet, bool add)
        {
            EstateUnit item = outlet ? _outletRepository.GetItem(unitId) : _warehouseRepository.GetItem(unitId);
            if (item != null && outlet)
            {
                var outletProduct = _outletProductRepository.GetItem(productId, unitId);
                if (outletProduct != null)
                {
                    outletProduct.Count = add ? outletProduct.Count + count : count;
                    _outletProductRepository.UpdateItem(outletProduct);
                }
                else
                {
                    _outletProductRepository.CreateItem(new OutletProduct { ProductId = productId, UnitId = unitId, Count = count });
                }

                return 1;
            }
            else
            {
                if (item == null)
                {
                    return 0;
                }

                var warehouseProduct = _warehouseProductRepository.GetItem(productId, unitId);
                if (warehouseProduct != null)
                {
                    warehouseProduct.Count = add ? warehouseProduct.Count + count : count;
                    _warehouseProductRepository.UpdateItem(warehouseProduct);
                }
                else
                {
                    _warehouseProductRepository.CreateItem(new WarehouseProduct { ProductId = productId, UnitId = unitId, Count = count });
                }

                return 1;
            }
        }
    }
}
