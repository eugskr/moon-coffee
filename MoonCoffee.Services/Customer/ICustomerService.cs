using System.Collections.Generic;

namespace MoonCoffee.Services.Customer
{
    public interface ICustomerService
    {
        List<Data.Models.Customer> GetAllCustomers(); 
        ServiceResponse<bool> DeleteCustomer(int id);
        ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer);

        Data.Models.Customer GetById(int id);

    }
}