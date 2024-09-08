namespace Catalog.API.Dtos.ProductsDto
{
    public record PaginateProductsSoldDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public double Price { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int SoldQuantity { get; set; }
        public float AverageRating { get; set; }
        public int DiscountPercentage { get; set; }
    }
}
