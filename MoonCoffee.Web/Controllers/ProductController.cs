using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoonCoffee.Services.Product;
using MoonCoffee.Web.Serialization;

namespace MoonCoffee.Web.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ILogger<ProductController> _logger;
        private IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;

        }
        [HttpGet("api/products")]
        public ActionResult GetAllProducts()
        {
            _logger.LogInformation("Getting all products");
            var products = _productService.GetAllProducts();
            var productModel = products.Select(ProductMapper.SerializeProduct);
            return Ok(productModel);
        }
    }
}