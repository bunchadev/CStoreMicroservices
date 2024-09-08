namespace Catalog.API.Dtos.ProductsDto
{
    public record PaginateProductsReq
    (
        string? Field,
        string? Order,
        int Page,
        int Limit,
        float Rating,
        Guid? Id
    );

}
