using MoonCoffee.Data.Models;
using MoonCoffee.Web.ViewModels;

namespace MoonCoffee.Web.Serialization
{
    public class ProductMapper
    {
        public static ProductModel SerializeProduct(Product product)
        {
            return new ProductModel
            {
                Id = product.ProductId,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }
        public static Product SerializeProduct(ProductModel product)
        {
            return new Product
            {
                ProductId = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }
    }
}