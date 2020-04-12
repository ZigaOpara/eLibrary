using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eLibrary.Providers.BookProvider;
using eLibrary.Providers.ReservationProvider;
using eLibrary.Providers.UserProvider;
using eLibrary.Repositories;
using eLibrary.Repositories.BookRepository;
using eLibrary.Repositories.ReservationRepository;
using eLibrary.Repositories.UserRepository;
using eLibrary.Services.BookService;
using eLibrary.Services.ReservationService;
using eLibrary.Services.UserService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eLibrary
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

            services.AddControllers();

            services.AddTransient(_ => new AppDb(Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb")));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IReservationService, ReservationService>();

            services.AddScoped<IUserProvider, UserProvider>();
            services.AddScoped<IBookProvider, BookProvider>();
            services.AddScoped<IReservationProvider, ReservationProvider>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}