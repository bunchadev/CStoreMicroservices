using System.Data;

namespace Catalog.API.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection Create();
    }
}
