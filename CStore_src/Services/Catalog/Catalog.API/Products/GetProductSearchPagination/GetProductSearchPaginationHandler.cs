using Catalog.API.Dtos.ProductsDto;
using Catalog.API.Repositories;
using CommonLib.CQRS;
using CommonLib.Response;

namespace Catalog.API.Products.GetPaginationProductsSearch
{
    public record GetProductSearchPaginationQuery
    (
        string? Field,
        string? Order,
        int Page,
        int Limit,
        float Rating,
        string? Title
    ) : IQuery<PaginatedResult<PaginateProductsSoldDto>>;
    internal class GetProductSearchPaginationHandler
        (IProductRepository productRepository)
          : IQueryHandler<GetProductSearchPaginationQuery, PaginatedResult<PaginateProductsSoldDto>>
    {
        public async Task<PaginatedResult<PaginateProductsSoldDto>> Handle(GetProductSearchPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = request.Adapt<PaginateSearchProductsReq>();
            var products = await productRepository.GetProductSearchPagination(query);
            return products;
        }
    }
}
