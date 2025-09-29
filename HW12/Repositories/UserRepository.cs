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
                .AsNoTracking()
                .Include(p => p.BorrowedBooks)
                .ThenInclude(p => p.Book)
                .ToList();
            return users;
        }
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public User GetUserByUsername(string username)
        {
            return _context.Users
                .AsNoTracking()
                .FirstOrDefault(p => p.Username == username);
        }
        public User GetUserById(int userid)
        {
            return _context.Users
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == userid);
        }
        public List<BorrowedBook> GetUserBorrowedBooks(int userid)
        {
            var user = _context.Users
                .AsNoTracking()
                .Include(p => p.BorrowedBooks)
                .ThenInclude(p => p.Book)
                .FirstOrDefault(u => u.Id == userid);
            return user.BorrowedBooks;
        }
        public List<Review> GetUserReviews(int userid)
        {
            var user = _context.Users
                .AsNoTracking()
                .Include(p => p.Reviews)
                .ThenInclude(p => p.Book)
                .FirstOrDefault(u => u.Id == userid);
            return user.Reviews;
        }
        public List<Wishlist> UserWishlists(int userid)
        {
            var user = _context.Users
                .AsNoTracking()
                .Include(p => p.Wishlist)
                .ThenInclude(p=>p.Book)
                .FirstOrDefault(u => u.Id == userid);
            return user.Wishlist;
        }
        public void ChangePenaltyAmount(int userid, decimal penaltyamount)
        {
            var user = GetUserById(userid);
            if (user != null)
            {
                user.PenaltyAmount = penaltyamount;
                _context.SaveChanges();
            }
        }
    }
}
