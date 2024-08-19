using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using User.API.Data;

namespace UserService.Tests.Abstractions
{
    public abstract class BaseIntegrationTest
    : IClassFixture<IntegrationTestWebAppFactory>,
      IDisposable
    {
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly IDbConnection Connection;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();

            var sqlConnectionFactory = _scope.ServiceProvider
                .GetRequiredService<ISqlConnectionFactory>();

            Connection = sqlConnectionFactory.Create();
        }
        public void Dispose()
        {
            _scope?.Dispose();
            Connection?.Dispose();
        }
    }
}


