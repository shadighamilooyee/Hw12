using HW12.Entities;
using HW12.Infrastructure;
using HW12.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HW12.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context = new();
        public List<Book> GetAllBooks()
        {
            var books = _context.Books
                .Include(p => p.Category)
                .ToList();
            return books;
        }
        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
        public Book GetBookById(int id)
        {
            return _context.Books
                .Include(b => b.Category)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefault(p => p.Id == id);
        }
        public void ChangeIsBorrowed(Book book, bool isborrowed)
        {
            var trackedBook = _context.Books.FirstOrDefault(b => b.Id == book.Id);
            if (trackedBook != null)
            {
                trackedBook.IsBorrowed = isborrowed;
                _context.SaveChanges();
            }
            //book.IsBorrowed = isborrowed;
            //_context.SaveChanges();
        }
        
        public List<Book> GetBookByCategory(int categoryid)
        {
            return _context.Books
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryid)
                .ToList();
        }
    }
}
