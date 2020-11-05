using System.Collections.Generic;
using MoonCoffee.Data.Models;

namespace MoonCoffee.Services.Order
{
    public interface IOrderService
    {
        List<SalesOrder> GetAllOrders();
        ServiceResponse<bool> CreateOpenOrder(SalesOrder order);
        ServiceResponse<bool> MarkFulfilled(int id);


    }
}