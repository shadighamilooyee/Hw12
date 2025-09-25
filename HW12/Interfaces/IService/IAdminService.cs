using HW12.Entities;

namespace HW12.Interfaces.IService
{
    public interface IAdminService
    {
        void AddCategory(string name);
        void AddBook(string title, string author, int categoryid);
        List<Book> GetBooks();
        List<Category> GetCategories();
    }
}
