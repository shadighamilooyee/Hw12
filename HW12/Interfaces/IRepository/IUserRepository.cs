using HW12.Entities;

namespace HW12.Interfaces.IRepository
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        void AddUser(User user);
        User GetUserByUsername(string username);
        void AddToUserBorrowedBooks(BorrowedBook borrowedbook);
        List<BorrowedBook> GetUserBorrowedBooks(int userid);
        List<Review> GetUserReviews(int userid);
    }
}
