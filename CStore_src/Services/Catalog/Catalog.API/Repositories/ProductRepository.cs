using Catalog.API.Dtos.ProductsDto;
using CommonLib.Response;
using Dapper;

namespace Catalog.API.Repositories
{
    public class ProductRepository
        (ISqlConnectionFactory connectionFactory)
        : IProductRepository
    {
        public async Task<PaginatedResult<PaginateProductsSoldDto>> GetPaginationProductSold(PaginateProductsReq request)
        {
            var parameters = new
            {
                p_field = request.Field ?? "id",
                p_order = request.Order ?? "asc",
                p_page = request.Page ,
                p_limit = request.Limit,
                p_rating = request.Rating,
                p_genre_id = request.Id
            };
            var query = """
                  SELECT * FROM PaginateProductsSold(
                        @p_field, 
                        @p_order, 
                        @p_page, 
                        @p_limit, 
                        @p_rating,
                        @p_genre_id
                  )
                  """;
            using var connection = connectionFactory.Create();
            var products = await connection.QueryAsync<PaginateProductsSoldDto>(query, parameters);
            return new PaginatedResult<PaginateProductsSoldDto>(
                request.Page,
                request.Limit,
                products.Count(),
                products
            );
        }

        public async Task<ProductDetailsSold?> GetProductById(Guid id)
        {
            var query = """
                   SELECT 
                    p.id as Id,
                    p.title as Title,
                    p.author as Author,
                    p.publisher as Publisher,
                    p.publication_year as PublicationYear,
                    p.page_count as PageCount,
                    p.dimensions as Dimensions,
                    p.cover_type as CoverType,
                    p.price as Price,
                    p.description as Description,
                    p.image_url as ImageUrl,
                    p.sold_quantity as SoldQuantity,
                    p.average_rating as AverageRating,
                    p.quantity_evaluate as QuantityEvaluate,
                    p.discount_percentage as DiscountPercentage
                FROM 
                    products p
                WHERE 
                    p.id = @Id;
                """;
            using var connection = connectionFactory.Create();
            return await connection.QueryFirstOrDefaultAsync<ProductDetailsSold>(query, new { Id = id });
        }

        public async Task<ProductDetailsSold?> GetProductByIdAndGenres(Guid id)
        {
            var query = """
                SELECT 
                   p.id as Id,
                   p.title as Title,
                   p.author as Author,
                   p.publisher as Publisher,
                   p.publication_year as PublicationYear,
                   p.page_count as PageCount,
                   p.dimensions as Dimensions,
                   p.cover_type as CoverType,
                   p.price as Price,
                   p.description as Description,
                   p.image_url as ImageUrl,
                   p.sold_quantity as SoldQuantity,
                   p.average_rating as AverageRating,
                   p.quantity_evaluate as QuantityEvaluate,
                   p.discount_percentage as DiscountPercentage,
                   g.name AS GenreName
                FROM  
                    products p
                LEFT JOIN 
                    product_genres pg ON p.id = pg.product_id
                LEFT JOIN 
                    genres g ON pg.genre_id = g.id
                WHERE 
                    p.id = @Id
            """;
            var productDictionary = new Dictionary<Guid, ProductDetailsSold>();
            using var connection = connectionFactory.Create();
            var result = await connection.QueryAsync<ProductDetailsSold, string, ProductDetailsSold>(
                query,
                (product, genre) =>
                {
                    if (!productDictionary.TryGetValue(product.Id, out var productEntry))
                    {
                        productEntry = product;
                        productEntry.Genres = new List<string>();
                        productDictionary.Add(productEntry.Id, productEntry);
                    }

                    if (genre != null && productEntry.Genres is not null)
                    {
                        productEntry.Genres.Add(genre);
                    }

                    return productEntry;
                },
                new { Id = id },
                splitOn: "GenreName"
            );
            return productDictionary.Values.FirstOrDefault();
        }

        public async Task<PaginatedResult<PaginateProductsSoldDto>> GetProductSearchPagination(PaginateSearchProductsReq request)
        {
            var parameters = new
            {
                p_field = request.Field ?? "id",
                p_order = request.Order ?? "asc",
                p_page = request.Page,
                p_limit = request.Limit,
                p_rating = request.Rating,
                p_search_term = request.Title
            };
            var query = """
                  SELECT * 
                  FROM ProductSearchPagination(
                        @p_field, 
                        @p_order, 
                        @p_page, 
                        @p_limit, 
                        @p_rating,
                        @p_search_term
                  )
                """;
            using var connection = connectionFactory.Create();
            var products = await connection.QueryAsync<PaginateProductsSoldDto>(query, parameters);
            return new PaginatedResult<PaginateProductsSoldDto>(
                request.Page,
                request.Limit,
                products.Count(),
                products
            );
        }
    }
}
