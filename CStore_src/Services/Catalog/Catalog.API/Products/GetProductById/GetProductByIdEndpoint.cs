using Catalog.API.Dtos.ProductsDto;
using CommonLib.Response;

namespace Catalog.API.Products.GetProductById
{
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/Product/{id}", async (Guid id, ISender sender) =>
            {
                var response = await sender.Send(new GetProductByIdQuery (id));
                if (response.Title is not null)
                    return Results.Ok(new Response<ProductDetailsSold>(
                            201,
                            "Get Product By Id",
                            response
                    ));
                else
                    return Results.Ok(new Response<ProductDetailsSold?>(
                            301,
                            "No products found",
                            null
                    ));
            });
        }
    }
};

