using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoonCoffee.Data;
using MoonCoffee.Data.Models;
using MoonCoffee.Services.Inventory;
using MoonCoffee.Services.Product;

namespace MoonCoffee.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly MoonDbContext _db;
        private readonly ILogger<OrderService> _logger;
        private readonly IInventoryService _inventoryService;
        private readonly IProductService _productService;
        public OrderService(MoonDbContext db, ILogger<OrderService> logger, IInventoryService inventoryService, IProductService productService)
        {
            _db = db;
            _logger = logger;
            _inventoryService = inventoryService;
            _productService = productService;

        }
        public ServiceResponse<bool> CreateOpenOrder(SalesOrder order)
        {
            var time = DateTime.UtcNow;
            _logger.LogInformation("Generating new Order");
            foreach (var item in order.SalesOrderItems)
            {
                item.Product = _productService.GetProductById(item.Product.ProductId);
                var inventoryId = _inventoryService.GetByProductId(item.Product.ProductId).Id;
                _inventoryService.UpdateUnitsAvailable(inventoryId, -item.Quantity);
            }
            try
            {
                _db.SalesOrders.Add(order);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    Time = time,
                    Message = "Open order created"
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    Time = time,
                    Message = e.StackTrace
                };
            }
        }

        public List<SalesOrder> GetAllOrders()
        {
            return _db.SalesOrders
            .Include(so => so.Customer)
            .ThenInclude(customer => customer.PrimaryAddress)
            .Include(so => so.SalesOrderItems)
            .ThenInclude(item => item.Product)
            .ToList();
        }

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            var time = DateTime.UtcNow;
            var order = _db.SalesOrders.Find(id);
            order.UpdatedOn = time;
            order.IsPaid = true;
            try
            {
                _db.SalesOrders.Update(order);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    Time = time,
                    Message = $"Order {id} closed: Invoice paid in full"
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    Time = time,
                    Message = e.StackTrace


                };
            }
        }
    }
}