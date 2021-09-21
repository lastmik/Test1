using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Test.Auyth.Api.Data;

namespace Test.Auyth.Api
{
    public class Startup
    {
        private IConfigurationRoot _connstr;
        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _connstr = new ConfigurationBuilder().SetBasePath(hostEnvironment.ContentRootPath).AddJsonFile("appsettings.json").Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_connstr.GetConnectionString("DefaultConnection")));
            services.AddTransient<IUsersRepository, UserRepository>();
            services.AddTransient<IJwtService, JwtService>();
            var authOptionsConfiguration = Configuration.GetSection("Auth");
            services.Configure<AuthOptions>(authOptionsConfiguration);
            var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,

                    ValidateLifetime = true,

                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true

                };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestApi", Version = "v1" });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Test.Auyth.Api.xml");
                c.IncludeXmlComments(filePath);
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(bulder =>
                {
                    bulder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => { 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestApi");
            });
            app.UseCors();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
