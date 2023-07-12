using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.ProductOrderQty.Factory;
using Nop.Plugin.Widgets.ProductOrderQty.Service;

namespace Nop.Plugin.Widgets.ProductOrderQty.Infrastructure
{
    public class NopStartup : INopStartup
    {
        public int Order => 0;

        public void Configure(IApplicationBuilder application)
        {
            
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductOrderQtyService, ProductOrderQtyService>();
            services.AddScoped<IProductOrderQtyModelFactory, ProductOrderQtyModelFactory>();
        }
    }
}
