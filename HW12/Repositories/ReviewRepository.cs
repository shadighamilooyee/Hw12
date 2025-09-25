using HW12.Entities;
using HW12.Infrastructure;
using HW12.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HW12.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context = new();
        public List<Review> GetAllReviews()
        {
            var _context = new AppDbContext();
            var reviews = _context.Reviews
                .Include(p => p.User)
                .Include(p => p.Book)
                .ToList();
            return reviews;
        }
        public void AddReview(Review review)
        {
            var _context = new AppDbContext();
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }
        public void DeleteReview(int id)
        {
            var _context = new AppDbContext();
            var review = GetReviewById(id);
            _context.Remove(review);
            _context.SaveChanges();
        }
        public Review GetReviewById(int id)
        {
            var _context = new AppDbContext();
            return _context.Reviews
                .Include(p => p.User)
                .Include(p => p.Book)
                .FirstOrDefault(p => p.Id == id);
        }
        public void ChangeIsApproved(Review review, bool isApproved)
        {
            var _context = new AppDbContext();
            review.IsApproved = isApproved;
            _context.SaveChanges();
        }
        public void ChangeComment(Review review, string newcomment)
        {
            var _context = new AppDbContext();
            review.Comment = newcomment;
            _context.SaveChanges();
        }
        public void ChangeRating(Review review, float newrating)
        {
            var _context = new AppDbContext();
            review.Rating = newrating;
            _context.SaveChanges();
        }
    }
}
