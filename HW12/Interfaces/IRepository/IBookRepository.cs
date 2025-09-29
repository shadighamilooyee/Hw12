using HW12.Entities;

namespace HW12.Interfaces.IRepository
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        void AddBook(Book book);
        Book GetBookById(int id);
        void ChangeIsBorrowed(int bookid, bool isborrowed);
        List<Book> GetBookByCategory(int categoryid);
    }
}
