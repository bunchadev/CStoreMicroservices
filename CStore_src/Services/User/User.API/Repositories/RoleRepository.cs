using Dapper;
using User.API.Data;

namespace User.API.Repositories
{
    public class RoleRepository(ISqlConnectionFactory connectionFactory) : IRoleRepository
    {
        public async Task<Guid?> GetRoleIdWithName(string name)
        {
            string query = "SELECT role_id FROM roles WHERE role_name = @Name";
            using var connection = connectionFactory.Create();
            return await connection.QueryFirstOrDefaultAsync<Guid>(
                query, 
                new { Name = name }
            );
        }
    }
}
