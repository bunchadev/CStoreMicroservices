using Catalog.API.Dtos.ProductsDto;
using CommonLib.Response;

namespace Catalog.API.Products.GetPaginationProductsSearch
{
    public class GetProductSearchPaginationEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/Product/search", async ([AsParameters] PaginateSearchProductsReq request,ISender sender) =>
            {
                var query = request.Adapt<GetProductSearchPaginationQuery>();
                var response = await sender.Send(query);
                return Results.Ok(new Response<PaginatedResult<PaginateProductsSoldDto>>(
                        201,
                        "Get List Product",
                        response
                ));
            });
        }
    }
}
