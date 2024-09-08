using Catalog.API.Dtos.ProductsDto;
using CommonLib.Response;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<PaginatedResult<PaginateProductsSoldDto>> GetPaginationProductSold(PaginateProductsReq request);
        Task<PaginatedResult<PaginateProductsSoldDto>> GetProductSearchPagination(PaginateSearchProductsReq request);
        Task<ProductDetailsSold?> GetProductByIdAndGenres(Guid id);
        Task<ProductDetailsSold?> GetProductById(Guid id);
    }
}
