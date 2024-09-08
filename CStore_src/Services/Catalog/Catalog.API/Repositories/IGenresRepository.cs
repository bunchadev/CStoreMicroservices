using Catalog.API.Dtos.GenresDto;

namespace Catalog.API.Repositories
{
    public interface IGenresRepository
    {
        Task<IEnumerable<GenresProductDto>> GetGenresByProduct(Guid id);
    }
}
