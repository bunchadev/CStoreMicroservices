using Catalog.API.Dtos.ProductsDto;
using CommonLib.Response;

namespace Catalog.API.Products.GetPaginationProductsSold
{
    public class GetPaginationProductsSoldEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/Product/home", async ([AsParameters] PaginateProductsReq request,ISender sender) =>
            {
                var query = request.Adapt<GetPaginationProductsSoldQuery>();
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

