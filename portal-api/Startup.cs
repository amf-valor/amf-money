using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Data.Model;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;

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
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = CredentialsHandler.Credentials.Key,
                ValidateIssuer = true,
                ValidIssuer = Token.Issuer,
                ValidateAudience = true,
                ValidAudience = Token.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            services.AddHttpContextAccessor();
            services.AddDbContext<AmfMoneyContext>(options => options.UseMySql(connectionString));
            services.AddTransient<ITradingBookService, TradingBookService>();
            services.AddTransient<IAccountService, AccountService>();
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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = tokenValidationParameters;
           });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });


            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TradingBookEntity, TradingBook>();
            });
            
            services.AddSingleton(config.CreateMapper());
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

            app.UseAuthentication();
            app.UseCors(corsPolicyName);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
