using Catalog.API.Dtos.GenresDto;

namespace Catalog.API.Dtos.ProductsDto
{
    public record ProductDetailsSold
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string? Publisher { get; set; } /* Tên nhà xuất bản */
        public int PublicationYear { get; set; } /* Năm xuất bản */
        public int PageCount { get; set; } /* Số trang */
        public string? Dimensions { get; set; } /* Kích thước sách */
        public string? CoverType { get; set; } /* Loại bìa */
        public double Price { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int SoldQuantity { get; set; } // Số lượng bán
        public float AverageRating { get; set; } // Đánh giá trung bình
        public int QuantityEvaluate { get; set; } /* Số lượng đánh giá */
        public int DiscountPercentage { get; set; } // Mã giảm giá riêng
        public ICollection<string>? Genres {  get; set; }
    }
}


