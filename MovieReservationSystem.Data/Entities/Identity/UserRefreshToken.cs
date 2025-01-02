namespace MovieReservationSystem.Data.Entities.Identity
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public string userID { get; set; } = default!;
        public string? Token { get; set; } = default!;
        public string? RefreshToken { get; set; } = default!;
        public string? JwtId { get; set; } = default!;
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual User User { get; set; } = new();
    }
}
