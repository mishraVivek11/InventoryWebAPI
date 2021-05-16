using AutoMapper;
using Inventory.BusinessLogic.Services;
using Inventory.DataAccess;
using Inventory.Models.ServiceInterfaces;
using InventoryWebAPI.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SGTIN.Handler;

namespace InventoryWebAPI
{
    public class Startup
    {
        private readonly JsonSerializerSettings dtoConverterSettings = new JsonSerializerSettings()
        {
            Converters = DtoJsonConverters.GetDtoJsonConverters()
        };

        /// <summary>
        /// Gets configuration service
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets Webhosting instance information
        /// </summary>
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters = this.dtoConverterSettings.Converters;
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            if (Environment.EnvironmentName == "Development_latest")
            {
                //Swagger configuration
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "InventoryWebAPI",
                        Version = "v1"
                    });
                });
            }
            string connectionString = this.Configuration["db_user"];

            services.AddDbContext<ADContext>(options => options.UseSqlServer(connectionString).UseLazyLoadingProxies());
            services.AddScoped<IReadRepository, BaseRepository>();
            services.AddScoped<IWriteRepository, WriteRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<ISGTINConverter, SGTIN96>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "InventoryAPI");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
