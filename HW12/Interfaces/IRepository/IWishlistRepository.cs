
using HW12.Entities;

namespace HW12.Interfaces.IRepository
{
    public interface IWishlistRepository
    {
        List<Wishlist> GetAllWishlists();
        void AddWishlist(Wishlist wishlist);
        void DeleteWishlist(int wishlistid);
        Wishlist GetWishlistById(int id);
        List<Wishlist> GetWishlistByBookId(int bookid);
    }
}
