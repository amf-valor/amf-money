using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Services;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AmfValor.AmfMoney.PortalApi
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        private readonly string corsPolicyName = "portalApiCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["MySqlConnection:local"];
            services.AddDbContext<AmfMoneyContext>(options => options.UseMySql(connectionString));
            services.AddTransient<ITradingBookService, TradingBookService>();
            services.AddTransient<IUserService, UserService>();
            services.AddApiVersioning();

            services.AddCors(options => 
            {
                options.AddPolicy(corsPolicyName, builder => 
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(corsPolicyName);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
