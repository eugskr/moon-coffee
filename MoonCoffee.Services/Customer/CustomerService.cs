using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoonCoffee.Data;

namespace MoonCoffee.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly MoonDbContext _db;
        public CustomerService(MoonDbContext db)
        {
            _db = db;
        }
        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            try
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Customer>
                {
                    Time = DateTime.UtcNow,
                    IsSuccess = true,
                    Data = customer,
                    Message = "New customer added"
                };

            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Customer>
                {
                    Time = DateTime.UtcNow,
                    IsSuccess = false,
                    Data = customer,
                    Message = e.StackTrace
                };
            }
        }

        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            var customer = _db.Customers.Find(id);
            var time = DateTime.UtcNow;
            if (customer == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Time = time,
                    IsSuccess = false,
                    Message = "Customer to delete not found"
                };
            }
            try
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Data = true,
                    Time = time,
                    IsSuccess = true,
                    Message = "Customer was deleted"
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Time = time,
                    IsSuccess = false,
                    Message = e.StackTrace
                };
            }
        }

        public List<Data.Models.Customer> GetAllCustomers()
        {
            return _db.Customers
            .Include(customers => customers.PrimaryAddress)
            .OrderBy(customers => customers.LastName)
            .ToList();
        }

        public Data.Models.Customer GetById(int id)
        {
            return _db.Customers.Find(id);
        }
    }
}