
using HW12.Entities;
using HW12.Infrastructure;
using HW12.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HW12.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly AppDbContext _context = new();
        public List<Wishlist> GetAllWishlists()
        {
            var wishlists = _context.Wishlists
                .AsNoTracking()
                .Include(p => p.User)
                .Include(p => p.Book)
                .ToList();
            return wishlists;
        }
        public void AddWishlist(Wishlist wishlist)
        {
            _context.Wishlists.Add(wishlist);
            _context.SaveChanges();
        }
        public void DeleteWishlist(int wishlistid)
        {
            var wishlist = GetWishlistById(wishlistid);
            if (wishlist != null)
            {
                _context.Remove(wishlist);
                _context.SaveChanges();
            }
        }
        public Wishlist GetWishlistById(int id)
        {
            return _context.Wishlists
                .AsNoTracking()
                .Include(p => p.User)
                .Include(p => p.Book)
                .FirstOrDefault(p => p.Id == id);
        }
        public List<Wishlist> GetWishlistByBookId(int bookid)
        {
            return _context.Wishlists
                .AsNoTracking()
                .Include(p => p.User)
                .Include(p => p.Book)
                .Where(p => p.BookId == bookid)
                .ToList();
        }
    }
}
