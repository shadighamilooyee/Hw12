using HW12.Entities;
using HW12.Interfaces.IRepository;
using HW12.Interfaces.IService;
using HW12.LocalDb;
using HW12.Migrations;
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
        private readonly IWishlistRepository _wishlistrepo = new WishlistRepository();
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

            if (!book.IsBorrowed)
                throw new Exception("Book Already Borrowed");

            var borrowedbook = new BorrowedBook()
            {
                BookId = bookid,
                UserId = userid,
                BorrowedDate = DateTime.Now
            };

            _borrowedbookrepo.AddBorrowedBook(borrowedbook);
            _bookrepo.ChangeIsBorrowed(bookid, true);
        }
        public void ReturnBook(int borrowedbookid, int userid)
        {
            var borrowedbook = _borrowedbookrepo.GetBorrowedBookById(borrowedbookid);

            if (borrowedbook == null)
                throw new Exception("Book Not Found With This Id");
            
            _borrowedbookrepo.DeleteBorrowedBook(borrowedbookid);
            _bookrepo.ChangeIsBorrowed(borrowedbookid, false);
            ChangeUserPenaltyAmount(borrowedbook,userid);
        }
        public void ChangeUserPenaltyAmount(BorrowedBook borrowedbook, int userid)
        {
            DateTime returndate = DateTime.Now;
            TimeSpan delay =returndate - borrowedbook.BorrowedDate;
            decimal minutesDelayed = delay.Minutes;
            decimal PenaltyAmount = minutesDelayed * 10000;
            _userrepo.ChangePenaltyAmount(userid, PenaltyAmount);
        }
        public decimal GetUserPenaltyAmount(int userid)
        {
            var user = _userrepo.GetUserById(userid);
            return user.PenaltyAmount;
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
            _reviewrepo.ChangeComment(reviewid, newcomment);
        }
        public void ChangeUserRating(float newrating, int reviewid)
        {
            _reviewrepo.ChangeRating(reviewid, newrating);
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
        public void AddWishlist(int userid, int bookid)
        {
            var wishlist = new Wishlist()
            {
                UserId = userid,
                BookId = bookid,
                CreatedAt = DateTime.Now
            };
            _wishlistrepo.AddWishlist(wishlist);
        }
        public void DeleteWishlist(int wishlistid, int userid)
        {
            var wishlist = _wishlistrepo.GetWishlistById(wishlistid);

            if (wishlist.UserId != userid)
                throw new Exception("Wrong Wishlist Id");

            _wishlistrepo.DeleteWishlist(wishlistid);
        }
        public List<Wishlist> GetUserWishlists(int userid)
        {
            var userwishlists = _userrepo.UserWishlists(userid);

            if (!userwishlists.Any())
                throw new Exception("You Did Not Add Any Wishlist Yet");

            return userwishlists;
        }
        public int BookWishlistCount(int bookid)
        {
            var wishlists = _wishlistrepo.GetWishlistByBookId(bookid);

            return wishlists.Count();
        }
    }
}
