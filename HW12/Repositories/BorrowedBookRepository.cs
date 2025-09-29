using HW12.Entities;
using HW12.Infrastructure;
using HW12.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HW12.Repositories
{
    public class BorrowedBookRepository : IBorrowedBookRepository
    {
        private readonly AppDbContext _context = new();
        public List<BorrowedBook> GetAllBorrowedBooks()
        {
            var borrowedbooks = _context.BorrowedBooks
                .Include(p => p.User)
                .Include(p => p.Book)
                .AsNoTracking().ToList();
            return borrowedbooks;
        }
        public void AddBorrowedBook(BorrowedBook borrowedbook)
        {
            _context.BorrowedBooks.Add(borrowedbook);
            _context.SaveChanges();
        }
        public void DeleteBorrowedBook(int borrowedbookid)
        {
            var borrowedbook = GetBorrowedBookById(borrowedbookid);
            if (borrowedbook != null)
            {
                _context.Remove(borrowedbook);
                _context.SaveChanges();
            }          
        }
        public BorrowedBook GetBorrowedBookById(int id)
        {
            return _context.BorrowedBooks
                .AsNoTracking()
                .Include(p => p.User)
                .Include(p => p.Book)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
