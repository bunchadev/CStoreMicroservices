using System.Data;

namespace User.API.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection Create();
    }
}
