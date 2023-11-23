using Application.Permisos.Create;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nest;
using Persistence;
using Persistence.Repositories;
using Web.Api.Options;

namespace Web.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

            builder.Services.AddDbContext<ApplicationDbContext>(
                (ServiceProvider, DbContextOptionsBuilder) =>
                {
                    DatabaseOptions databaseOptions = ServiceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;
                    DbContextOptionsBuilder.UseSqlServer(databaseOptions.ConnectionString, sqlServerAction =>
                    {
                        sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                        sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
                    });
                    DbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                    DbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
                });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region MediatR
            
            builder.Services.AddMediatR(media => media.RegisterServicesFromAssembly(typeof(Application.Permisos.Create.CreatePermisoCommandHandler).Assembly));
            builder.Services.AddTransient<CreatePermisoCommandHandler>();
            
            #endregion

            #region AppServices

            builder.Services.AddTransient<IPermisoRepository, PermisoRepository>();
            builder.Services.AddTransient<ITipoPermisoRepository, TipoPermisoRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            #endregion

            #region ElasticSearch

            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("default");
            var client = new ElasticClient(settings);
            builder.Services.AddSingleton<IElasticClient>(client);
            
            #endregion

            WebApplication app = builder.Build();

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
    }
}