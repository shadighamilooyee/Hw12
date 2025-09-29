using HW12.Entities;
using HW12.Infrastructure;
using HW12.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HW12.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context = new();
        public List<Category> GetAllCategories()
        {
            var categories = _context.Categories
                .AsNoTracking()
                .Include(p=> p.Books)
                .ToList();
            return categories;
        }
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public Category GetCategoryById(int id)
        {
            return _context.Categories
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
