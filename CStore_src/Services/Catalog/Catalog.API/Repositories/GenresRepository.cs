using Catalog.API.Dtos.GenresDto;
using Dapper;

namespace Catalog.API.Repositories
{
    public class GenresRepository
        (ISqlConnectionFactory connectionFactory)
        : IGenresRepository
    {
        public async Task<IEnumerable<GenresProductDto>> GetGenresByProduct(Guid id)
        {
            var query = """
               SELECT g.name AS Name
               FROM genres g
               JOIN product_genres pg ON g.id = pg.genre_id
               WHERE pg.product_id = @ProductId
            """;
            using var connection = connectionFactory.Create();
            return await connection.QueryAsync<GenresProductDto>(query, new { ProductId = id });
        }
    }
}
