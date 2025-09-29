using HW12.Enums;

namespace HW12.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
        public List<BorrowedBook>? BorrowedBooks { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<Wishlist>? Wishlist { get; set; }
        public decimal PenaltyAmount { get; set; }
    }
}
