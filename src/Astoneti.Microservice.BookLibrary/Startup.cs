using Astoneti.Microservice.BookLibrary.Business;
using Astoneti.Microservice.BookLibrary.Business.Contracts;
using Astoneti.Microservice.BookLibrary.Data;
using Astoneti.Microservice.BookLibrary.Data.Contracts;
using Astoneti.Microservice.BookLibrary.Mappings;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Astoneti.Microservice.BookLibrary
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookLibraryContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BookProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Astoneti.Microservice.BookLibrary", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Astoneti.Microservice.BookLibrary v1"));
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
