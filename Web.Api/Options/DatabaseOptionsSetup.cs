using Microsoft.Extensions.Options;

namespace Web.Api.Options
{
    public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
    {
        private const string ConfigurationSectionName = "DatabaseOptions";
        private const string environment = "ASPNETCORE_ENVIRONMENT";
        private readonly IConfiguration _configuration;

        public DatabaseOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(DatabaseOptions options)
        {
            var env = Environment.GetEnvironmentVariable(environment);
            if (env == "dev")
            {
                var connectionString = _configuration.GetConnectionString("Database");

                options.ConnectionString = connectionString!;

            }
            else
            {
                var host = Environment.GetEnvironmentVariable("DB_HOST");
                var name = Environment.GetEnvironmentVariable("DB_NAME");
                var pwd = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

                options.ConnectionString = $"Data Source={host};Initial Catalog={name};User Id=sa;Password={pwd};TrustServerCertificate=True;";
            }

            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}
