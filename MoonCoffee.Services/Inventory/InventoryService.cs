using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoonCoffee.Data;
using MoonCoffee.Data.Models;

namespace MoonCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly MoonDbContext _db;
        private readonly ILogger<InventoryService> _logger;
        public InventoryService(MoonDbContext db, ILogger<InventoryService> logger)
        {
            _db = db;
            _logger = logger;
        }
        protected void CreateSnapshot(ProductInventory inventory)
        {
            var snapshot = new ProductInventorySnapshot
            {
                Product = inventory.Product,
                SnapshotTime = DateTime.UtcNow,
                QuantityOnHand = inventory.QuantityOnHand
            };
            _db.Add(snapshot);
        }

        public ProductInventory GetByProductId(int productId)
        {
            return _db.ProductInventories
            .Include(x => x.Product)
            .FirstOrDefault(x => x.Product.ProductId == productId);
        }

        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
            .Include(x => x.Product)
            .Where(x => !x.Product.IsArchived)
            .ToList();
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var time = DateTime.UtcNow-TimeSpan.FromHours(6);
            return _db.ProductInventorySnapshots
            .Include(x=>x.Product)
            .Where(x=>x.SnapshotTime>time && !x.Product.IsArchived).ToList();
        }

        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            var time = DateTime.UtcNow;
            try
            {
                var inventory = _db.ProductInventories
                .Include(x => x.Product)
                .First(x => x.Product.ProductId == id);

                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapshot(inventory);
                }
                catch (Exception e)
                {
                    _logger.LogError("Error creating Inventory snapshot");
                    _logger.LogError(e.StackTrace);

                }
                _db.SaveChanges();

                return new ServiceResponse<ProductInventory>
                {
                    Data = inventory,
                    IsSuccess = true,
                    Time = time,
                    Message = $"Product {inventory.Product.Name} adjusted"

                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProductInventory>
                {
                    Data = null,
                    IsSuccess = false,
                    Time = time,
                    Message = e.StackTrace

                };
            }
        }
    }
}