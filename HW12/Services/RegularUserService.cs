using HW12.Entities;
using HW12.Interfaces.IRepository;
using HW12.Interfaces.IService;
using HW12.LocalDb;
using HW12.Repositories;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace HW12.Services
{
    public class RegularUserService : IRegularUserService
    {
        private readonly ICategoryRepository _categoryrepo = new CategoryRepository();
        private readonly IBookRepository _bookrepo = new BookRepository();
        private readonly IBorrowedBookRepository _borrowedbookrepo = new BorrowedBookRepository();
        private readonly IUserRepository _userrepo = new UserRepository();
        private readonly IReviewRepository _reviewrepo = new ReviewRepository();
        public List<Category> GetCategories()
        {
            var categories = _categoryrepo.GetAllCategories();

            if (!categories.Any())
                throw new Exception("No Category Found");

            return categories;
        }
        public List<Book> GetBooksByCategory(int categoryid)
        {
            var books = _bookrepo.GetBookByCategory(categoryid);
            
            if (!books.Any())
                throw new Exception("No Books Found In This Category");

            return books;
        }
        public List<Book> GetBooks()
        {
            var books = _bookrepo.GetAllBooks();

            if (!books.Any())
                throw new Exception("No Books Found");

            return books;
        }
        public void BorrowBook(int bookid, int userid)
        {
            var book = _bookrepo.GetBookById(bookid);

            if (book == null)
                throw new Exception("Book Not Found With This Id");

            if (book.IsBorrowed)
                throw new Exception("Book Already Borrowed");

            var borrowedbook = new BorrowedBook()
            {
                BookId = bookid,
                UserId = userid,
                BorrowedDate = DateTime.Now
            };

            _borrowedbookrepo.AddBorrowedBook(borrowedbook);
            _bookrepo.ChangeIsBorrowed(book, true);
            //_userrepo.AddToUserBorrowedBooks(borrowedbook);
        }
        public List<BorrowedBook> UserBorrowedBooks(int userid)
        {
            var userbooks = _userrepo.GetUserBorrowedBooks(userid);

            if (!userbooks.Any())
                throw new Exception("You Did Not Borrowed Any Books Yet");

            return userbooks;
        }
        public void AddUserReview(int userid, int bookid, string comment, float rating)
        {
            if (rating < 0 || rating > 5)
                throw new Exception("Wrong Rating");
            var review = new Review()
            {
                UserId = userid,
                BookId = bookid,
                Comment = comment,
                Rating = rating,
                CreatedAt = DateTime.Now
            };
            _reviewrepo.AddReview(review);
        }
        public List<Review> GetUserReviews(int userid)
        {
            var userreviews = _userrepo.GetUserReviews(userid);

            if (!userreviews.Any())
                throw new Exception("You Did Not Add any Reviews Yet");

            return userreviews;
        }
        public void DeleteUserReview(int reviewid, int userid)
        {
            var review = _reviewrepo.GetReviewById(reviewid);

            if (review.UserId != userid)
                throw new Exception("Wrong Review Id");

            _reviewrepo.DeleteReview(reviewid);
        }
        public void ChangeUserComment(string newcomment, int reviewid)
        {
            var review = _reviewrepo.GetReviewById(reviewid);
            _reviewrepo.ChangeComment(review, newcomment);
        }
        public void ChangeUserRating(float newrating, int reviewid)
        {
            var review = _reviewrepo.GetReviewById(reviewid);
            _reviewrepo.ChangeRating(review, newrating);
        }
        public List<Review> ShowBooksReviews(int bookid)
        {
            var book = _bookrepo.GetBookById(bookid);

            if (book == null)
                throw new Exception("Book Not Found");

            var bookreviews = book.Reviews.Where(p=>p.IsApproved==true).ToList();
            return bookreviews;
        }
        public float BookAvgRating(int bookid)
        {
            var book = _bookrepo.GetBookById(bookid);

            if (book == null)
                throw new Exception("Book Not Found");

            var approvedReviews = book.Reviews?.Where(p => p.IsApproved).ToList();

            if (!approvedReviews.Any())
                return 0;

            var avgrating = approvedReviews.Average(p=>p.Rating);

            return avgrating;
        }
    }
}
