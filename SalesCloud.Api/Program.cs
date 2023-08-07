using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SalesCloud.Api.Extensions;
using SalesCloud.Common.Dtos.Ccp;
using SalesCloud.Data.Contracts;
using SalesCloud.Domain.Contracts;
using SalesCloud.Infrastructure.Context;
using SalesCloud.Infrastructure.Logger;
using SalesCloud.Infrastructure.Repository;
using SalesCloud.Logic.Contracts;
using SalesCloud.Logic.Contracts.Integrations;
using SalesCloud.Logic.Mapping;
using SalesCloud.Logic.Services;
using SalesCloud.Logic.Services.Integrations;
using SalesCloud.Logic.Validators;

namespace SalesCloud.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            RegisterHttpClients(builder.Services, builder.Configuration);
            RegisterServices(builder.Services);
            RegisterValidators(builder.Services);
            ConfigureDbContext(builder.Services, builder.Configuration);

            var app = builder.Build();

            var logger = app.Services.GetRequiredService<ILoggerManager>();
            app.ConfigureExceptionHandler(logger);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"));
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ISoftwareService, SoftwareService>();
            services.AddTransient<ICcpService, CcpService>();
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        private static void RegisterValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<PurchaseSoftwareRequest>, PurchaseSoftwareRequestValidator>();
            services.AddTransient<IValidator<UpdateLicenseQuantityRequest>, UpdateLicenseQuantityValidator>();
            services.AddTransient<IValidator<ExtendLicenseRequest>, ExtendLicenseRequestValidator>();
        }

        private static void RegisterHttpClients(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("CcpApiClient", client =>
            {
                client.BaseAddress = new Uri(configuration["Ccp:ApiUrl"] ?? throw new Exception("Ccp Api url is missing in config."));
                client.Timeout = TimeSpan.FromSeconds(30);
            });
        }
    }
}