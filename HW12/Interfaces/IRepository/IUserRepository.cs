using HW12.Entities;

namespace HW12.Interfaces.IRepository
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        void AddUser(User user);
        User GetUserForLogin(string username, string password);
        List<BorrowedBook> GetUserBorrowedBooks(int userid);
        List<Review> GetUserReviews(int userid);
        List<Wishlist> UserWishlists(int userid);
        void ChangePenaltyAmount(int userid, decimal penaltyamount);
        User GetUserById(int userid);
    }
}
