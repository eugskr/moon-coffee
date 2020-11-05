using System.Collections.Generic;
using MoonCoffee.Data.Models;

namespace MoonCoffee.Services.Inventory
{
    public interface IInventoryService
    {
        List<ProductInventory> GetCurrentInventory();
        ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment);
        ProductInventory GetByProductId(int productId);
      
        List<ProductInventorySnapshot> GetSnapshotHistory();
    }
}