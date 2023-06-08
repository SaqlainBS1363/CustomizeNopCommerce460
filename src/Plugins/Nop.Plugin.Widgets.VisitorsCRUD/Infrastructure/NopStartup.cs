using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.VisitorsCrud.Factory;
using Nop.Plugin.Widgets.VisitorsCrud.Service;

namespace Nop.Plugin.Widgets.VisitorsCrud.Infrastructure
{
    public class NopStartup : INopStartup
    {
        public int Order => 0;

        public void Configure(IApplicationBuilder application)
        {
            
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IVisitorService, VisitorService>();
            services.AddScoped<IVisitorModelFactory, VisitorModelFactory>();
        }
    }
}
