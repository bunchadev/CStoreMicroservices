namespace User.API.Models.Domain
{
    public class Token
    {
        public Guid TokenId { get; set; }
        public Guid UserId { get; set; }
        public string TokenValue { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
