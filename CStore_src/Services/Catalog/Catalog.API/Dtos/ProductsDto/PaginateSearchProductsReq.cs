namespace Catalog.API.Dtos.ProductsDto
{
    public record PaginateSearchProductsReq
    (
        string? Field,
        string? Order,
        int Page,
        int Limit,
        float Rating,
        string? Title
    );
}
