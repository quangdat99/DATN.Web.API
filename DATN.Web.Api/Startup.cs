using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Repo.Repo;
using DATN.Web.Service.Service;
using DATN.Web.Service.Middlewares;
using DATN.Web.Service.Contexts;
using System.IO;

namespace DATN.Web.Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DATN.Web.Api", Version = "v1" });
            });

            services.AddHttpContextAccessor();
            services.AddScoped(typeof(IBaseRepo), typeof(BaseRepo));
            services.AddScoped(typeof(IBaseService), typeof(BaseService));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepo, UserRepo>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepo, ProductRepo>();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepo, OrderRepo>();

            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAddressRepo, AddressRepo>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();

            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IColorRepo, ColorRepo>();


            services.AddScoped<IAttributeService, AttributeService>();
            services.AddScoped<IAttributeRepo, AttributeRepo>();

            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<ISizeRepo, SizeRepo>();

            services.AddScoped<IProductCartService, ProductCartService>();
            services.AddScoped<IProductCartRepo, ProductCartRepo>();

            services.AddScoped<IVoucherService, VoucherService>();
            services.AddScoped<IVoucherRepo, VoucherRepo>();
            services.AddScoped<IContextService, WebContextService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DATN.Web.Api v1"));
            }
            app.UseHttpsRedirection();
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseStaticFiles();

            // Sử dụng authen context
            app.UseSetAuthContextHandler();


            app.UseRouting();

            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
