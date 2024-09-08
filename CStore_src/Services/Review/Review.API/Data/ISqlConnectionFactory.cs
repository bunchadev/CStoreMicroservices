using System.Data;

namespace Review.API.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection Create();
    }
}
