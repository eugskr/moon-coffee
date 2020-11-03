using System.Collections.Generic;
using MoonCoffee.Data;
using System.Linq;
using System;
using MoonCoffee.Data.Models;
namespace MoonCoffee.Services.Product
{
    public class ProductService : IProductService
    {
        private MoonDbContext _db;
        public ProductService(MoonDbContext db)
        {
            _db = db;
        }
        public ServiceResponse<Data.Models.Product> ArchiveProduct(int id)
        {
            try
            {
                var product = _db.Products.Find(id);
                product.IsArchived = true;
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    IsSuccess = true,
                    Message = "Product achived"
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    IsSuccess = false,
                    Message = e.StackTrace
                };
            }

        }

        public ServiceResponse<Data.Models.Product> CreateProduct(Data.Models.Product product)
        {
            try
            {
                _db.Products.Add(product);

                var inventory = new ProductInventory
                {
                    Product = product,
                    CreatedOn = DateTime.UtcNow,
                    IdealQuantity = 10,
                    QuantityOnHand = 0

                };
                _db.ProductInventories.Add(inventory);

                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    IsSuccess = true,
                    Message = "Created new product"
                };

            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    IsSuccess = false,
                    Message = e.StackTrace
                };
            }
        }

        public List<Data.Models.Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        public Data.Models.Product GetProductById(int id)
        {
            return _db.Products.Find(id);
        }
    }
}