using HW12.Entities;
using HW12.Infrastructure;
using HW12.Interfaces.IRepository;
using HW12.LocalDb;
using Microsoft.EntityFrameworkCore;

namespace HW12.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context = new();
        public List<User> GetAllUsers()
        {
            var users = _context.Users
                .Include(p => p.BorrowedBooks)
                .ThenInclude(p => p.Book)
                .AsNoTracking().ToList();
            return users;
        }
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(p => p.Username == username);
        }
        public void AddToUserBorrowedBooks(BorrowedBook borrowedbook)
        {
            var user = _context.Users
                .Include(u => u.BorrowedBooks)
                .FirstOrDefault(u => u.Id == borrowedbook.UserId);
            if (user != null)
            {
                user.BorrowedBooks.Add(borrowedbook);
                _context.SaveChanges();
            }
            //_context.BorrowedBooks.Add(borrowedbook);
            //_context.SaveChanges();
        }
        public List<BorrowedBook> GetUserBorrowedBooks(int userid)
        {
            var user = _context.Users
                      .Include(p => p.BorrowedBooks)
                      .ThenInclude(p => p.Book)
                      .FirstOrDefault(u => u.Id == userid);
            return user.BorrowedBooks;
        }
        public List<Review> GetUserReviews(int userid)
        {
            var user = _context.Users
                      .Include(p => p.Reviews)
                      .ThenInclude(p => p.Book)
                      .FirstOrDefault(u => u.Id == userid);
            return user.Reviews;
        }
    }
}
