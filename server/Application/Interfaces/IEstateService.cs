namespace Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request.Estate.Outlet;
    using Application.DTO.Request.Estate.Warehouse;
    using Application.DTO.Response.Estate;

    public interface IEstateService
    {
        OutletDto CreateOutlet(OutletCreateRequestDto outlet);

        WarehouseDto CreateWarehouse(WarehouseCreateRequestDto warehouse);

        int DeleteOutlet(int outletId);

        int DeleteWarehouse(int warehouseId);

        List<OutletDto> GetOutlets(int warehouseId);

        List<WarehouseDto> GetWarehouses(int regionId);

        OutletDto UpdateOutlet(OutletUpdateRequestDto outlet);

        WarehouseDto UpdateWarehouse(WarehouseUpdateRequestDto warehouse);

        int SetProductCount(Guid productId, int unitId, bool outlet, int count);

        int AddProductCount(Guid productId, int unitId, bool outlet, int count);

        List<WarehouseDto> GetWarehousesByCity(int cityId);

        List<OutletDto> GetOutlets();

        List<WarehouseDto> GetWarehouses();

        WarehouseDto GetWarehouse(int id);

        OutletDto GetOutlet(int id);
    }
}