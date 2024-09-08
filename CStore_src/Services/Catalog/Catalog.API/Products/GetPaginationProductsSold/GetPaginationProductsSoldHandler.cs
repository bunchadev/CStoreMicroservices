using Catalog.API.Dtos.ProductsDto;
using Catalog.API.Repositories;
using CommonLib.CQRS;
using CommonLib.Response;

namespace Catalog.API.Products.GetPaginationProductsSold
{
    public record GetPaginationProductsSoldQuery(
        string? Field,
        string? Order,
        int Page,
        int Limit,
        float Rating,
        Guid? Id
    ) : IQuery<PaginatedResult<PaginateProductsSoldDto>>;
    internal class GetPaginationProductsSoldHandler
        (IProductRepository productRepository)
        : IQueryHandler<GetPaginationProductsSoldQuery, PaginatedResult<PaginateProductsSoldDto>>
    {
        public async Task<PaginatedResult<PaginateProductsSoldDto>> Handle(GetPaginationProductsSoldQuery request, CancellationToken cancellationToken)
        {
            var query = request.Adapt<PaginateProductsReq>();
            var products = await productRepository.GetPaginationProductSold(query);
            return products;
        }
    }
}
