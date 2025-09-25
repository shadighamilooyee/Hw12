using HW12.Entities;

namespace HW12.Interfaces.IRepository
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        void AddCategory(Category category);
        Category GetCategoryById(int id);
    }
}
