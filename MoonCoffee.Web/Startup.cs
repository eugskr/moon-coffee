using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MoonCoffee.Data;
using Microsoft.EntityFrameworkCore;
using MoonCoffee.Services.Product;
using MoonCoffee.Services.Inventory;
using MoonCoffee.Services.Customer;
using MoonCoffee.Services.Order;

namespace MoonCoffee.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<MoonDbContext>(options =>
            {
                options.EnableDetailedErrors();
                options.UseNpgsql(Configuration.GetConnectionString("moon.dev"));
            });
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IInventoryService, InventoryService>();

            services.AddTransient<ICustomerService, CustomerService>();

            services.AddTransient<IOrderService, OrderService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
