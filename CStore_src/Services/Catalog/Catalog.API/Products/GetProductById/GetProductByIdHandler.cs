using Catalog.API.Dtos.ProductsDto;
using Catalog.API.Repositories;
using CommonLib.CQRS;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<ProductDetailsSold>;
    internal class GetProductByIdHandler
        (IProductRepository productRepository)
        : IQueryHandler<GetProductByIdQuery, ProductDetailsSold>
    {
        public async Task<ProductDetailsSold> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var query = request.Adapt<GetProductByIdQuery>();
            return await productRepository
                   .GetProductByIdAndGenres(query.Id) 
                   ?? new ProductDetailsSold { };
        }
    }
}
