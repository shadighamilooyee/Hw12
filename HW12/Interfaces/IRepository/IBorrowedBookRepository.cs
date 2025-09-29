using HW12.Entities;

namespace HW12.Interfaces.IRepository
{
    public interface IBorrowedBookRepository
    {
        List<BorrowedBook> GetAllBorrowedBooks();
        void AddBorrowedBook(BorrowedBook borrowedbook);
        void DeleteBorrowedBook(int borrowedbookid);
        BorrowedBook GetBorrowedBookById(int id);
    }
}
