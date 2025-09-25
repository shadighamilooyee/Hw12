using HW12.Entities;

namespace HW12.Interfaces.IService
{
    public interface IRegularUserService
    {
        List<Category> GetCategories();
        List<Book> GetBooksByCategory(int categoryid);
        List<Book> GetBooks();
        void BorrowBook(int bookid, int userid);
        List<BorrowedBook> UserBorrowedBooks(int userid);
        void AddUserReview(int userid, int bookid, string comment, float rating);
        List<Review> GetUserReviews(int userid);
        void DeleteUserReview(int reviewid, int userid);
        void ChangeUserComment(string newcomment, int reviewid);
        void ChangeUserRating(float newrating, int reviewid);
        List<Review> ShowBooksReviews(int bookid);
        float BookAvgRating(int bookid);
    }
}
