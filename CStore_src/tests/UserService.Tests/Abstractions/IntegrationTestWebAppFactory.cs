using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Data;
using Testcontainers.MsSql;
using User.API.Data;
using User.API.Models.Dtos.UserDtos.Data;
using UserService.Tests.Data;

namespace UserService.Tests.Abstractions
{
    public class IntegrationTestWebAppFactory
    : WebApplicationFactory<Program>,
      IAsyncLifetime
    {
        private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server")
            .WithPassword("Strong_password_123!")
            .Build();

        private IDbConnection? _connection;
        private IConfiguration? _configuration;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "ConnectionStrings:Database", _dbContainer.GetConnectionString() }
                });
                _configuration = config.Build();
            });

            builder.ConfigureTestServices(services =>
            {
                var descriptorType =
                    typeof(ISqlConnectionFactory);

                var descriptor = services
                    .SingleOrDefault(s => s.ServiceType == descriptorType);

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }
                if(_configuration is not null)
                {
                    services.AddSingleton<ISqlConnectionFactory>(_ =>
                      new SqlConnectionFactory(_configuration));
                } 
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            var sqlConnectionFactory = Services.GetRequiredService<ISqlConnectionFactory>();
            _connection = sqlConnectionFactory.Create();

            await DatabaseInitializer.InitializeDatabaseAsync(_connection);
        }

        public new Task DisposeAsync()
        {
            return _dbContainer.StopAsync();
        }
    }
}


