using HW12.Entities;
using HW12.Interfaces.IRepository;
using HW12.Interfaces.IService;
using HW12.Repositories;

namespace HW12.Services
{
    public class AdminService : IAdminService
    {
        private readonly ICategoryRepository _categoryrepo = new CategoryRepository();
        private readonly IBookRepository _bookrepo = new BookRepository();
        private readonly IBorrowedBookRepository _borrowedbookrepo = new BorrowedBookRepository();
        private readonly IUserRepository _userrepo = new UserRepository();
        private readonly IReviewRepository _reviewrepo = new ReviewRepository();

        public void AddCategory(string name)
        {
            var category = new Category { Name = name };

            _categoryrepo.AddCategory(category);
        }
        public void AddBook(string title, string author, int categoryid)
        {
            var category = _categoryrepo.GetCategoryById(categoryid);

            if (category == null)
                throw new Exception("Category Not Found");

            var book = new Book()
            {
                Title = title,
                Author = author,
                CategoryId = categoryid
            };
            _bookrepo.AddBook(book);
        }
        public List<Book> GetBooks()
        {
            return _bookrepo.GetAllBooks();
        }
        public List<Category> GetCategories()
        {
            return _categoryrepo.GetAllCategories();
        }
        public List<Review> ShowBooksReviews(int bookid)
        {
            var book = _bookrepo.GetBookById(bookid);

            if (book == null)
                throw new Exception("Book Not Found");

            var bookreviews = book.Reviews.Where(p => p.IsApproved == true).ToList();
            return bookreviews;
        }
        public void ChangeIsApproved(int reviewid, bool isApproved)
        {
            var review = _reviewrepo.GetReviewById(reviewid);
            _reviewrepo.ChangeIsApproved(review, isApproved);
        }
        public List<Review> GetAllReviews()
        {
            return _reviewrepo.GetAllReviews();
        }
    }
}
