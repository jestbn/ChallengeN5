using Application.Behavior;
using Application.Permisos.Create;
using Confluent.Kafka;
using Domain.Abstractions;
using Infraestructure.ElasticSearch;
using Infraestructure.Kafka;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nest;
using Persistence;
using Persistence.Repositories;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Web.Api.Options;

namespace Web.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

            builder.Services.AddDbContext<ApplicationDbContext>(
                (serviceProvider, dbContextOptionsBuilder) =>
                {
                    var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;
                    dbContextOptionsBuilder.UseSqlServer(databaseOptions.ConnectionString, sqlServerAction =>
                    {
                        sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                        sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
                    });
                    dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                    dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
                });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            #region MediatR
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExternalBehaviour<,>));
            builder.Services.AddMediatR(media => media.RegisterServicesFromAssembly(typeof(Application.Permisos.Create.CreatePermisoCommandHandler).Assembly));
            builder.Services.AddTransient<CreatePermisoCommandHandler>();

            #endregion

            #region AppServices

            builder.Services.AddTransient<IPermisoRepository, PermisoRepository>();
            builder.Services.AddTransient<ITipoPermisoRepository, TipoPermisoRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            #region ElasticSearch & Serilog
            var elasticUri = new Uri(builder.Configuration[key: "ElasticConfiguration:Uri"]!);

            var elasticConfiguration = new ElasticsearchSinkOptions(elasticUri)
            {
                IndexFormat = $"{builder.Configuration["ApplicationName"]}-logs-{DateTime.UtcNow:yyyy-MM}",
            };

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(elasticConfiguration)
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            var connectionSettings = new ConnectionSettings(elasticUri);
            builder.Services.AddSingleton<IElasticService>(new ElasticService(connectionSettings));

            #endregion

            #region Kafka

            var kafkaConfig = new ProducerConfig()
            {
                BootstrapServers = builder.Configuration[key: "KafkaConfiguration"]
            };
            builder.Services.AddSingleton<IKafkaProducerService>(new KafkaProducerService(kafkaConfig));

            #endregion

            WebApplication app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!
                .CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (!context.Database.CanConnect()) context.Database.Migrate(); else context.Database.EnsureCreated();

            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}