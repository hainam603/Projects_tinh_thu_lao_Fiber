using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassModel.Connection;
using ClassModel.Connection.Connection;
using ClassModel.Connection.Sql;
using FiberSevices.Server.Fiber;
using FiberSevices.Server.Fiber.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FiberSevices
{
    public class Startup
    {
        private Connection _connection = new Connection();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(_connection.connectionStringSQL));
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IPTMFiber, PTMFiberImpl>();
            services.AddScoped<IThuLaoFiber, ThuLaoFiberImpl>();
            services.AddScoped<IFiberPTM, FiberPTMImpl>();
            services.AddScoped<IFiberThulao, FiberThulaoImpl>();
           
            var audienceConfig = Configuration.GetSection("Audience");

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Iss"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Aud"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "TestKey";
            })
            .AddJwtBearer("TestKey", x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = tokenValidationParameters;
            });
            // CORS

            //services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            //{
            //    builder.AllowAnyOrigin()
            //           .AllowAnyMethod()
            //           .AllowAnyHeader();
            //}));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder.WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });

            //DI
            services.AddSingleton<IFileProvider>(
               new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));//image

            //IFileProvider physicalProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());

            //services.AddSingleton<IFileProvider>(physicalProvider);


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }
            //CROS
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            // Enable directory browsing
            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\img")),
                RequestPath = new PathString("/img")
            });

            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\img")),
            //    RequestPath = new PathString("/img")
            //});
            app.UseMvc();
        }
    }
}
