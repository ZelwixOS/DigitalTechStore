namespace Application.DTO.Response
{
    using System;
    using Domain.Models;

    public class WorkerInfo
    {
        public WorkerInfo(User user, string role)
        {
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.FirstName = user.FirstName;
            this.SecondName = user.SecondName;
            this.Role = role;
            this.PhoneNumber = user.PhoneNumber;
            this.Email = user.Email;
            this.Avatar = user.Avatar;
            this.OutletId = user.OutletId;
            this.WarehouseId = user.WarehouseId;
            this.Banned = user.Banned;
            this.RegionId = user.Outlet != null ? user.Outlet.City.RegionId : user.Warehouse.City.RegionId;
            this.CityId = user.Outlet != null ? user.Outlet.CityId : user.Warehouse.CityId;
            this.UnitName = user.Outlet != null ? $"{user.Outlet.Name} ({user.Outlet.City.Name})" : $"{user.Warehouse.Name} ({user.Warehouse.City.Name})";
            this.OutletWorker = user.OutletId.HasValue;
        }

        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Avatar { get; set; }

        public int RegionId { get; set; }

        public int CityId { get; set; }

        public int? OutletId { get; set; }

        public int? WarehouseId { get; set; }

        public bool Banned { get; set; }

        public string UnitName { get; set; }

        public bool OutletWorker { get; set; }
    }
}
