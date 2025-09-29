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
            var reviews = _context.Reviews
                .AsNoTracking()
                .Include(p => p.User)
                .Include(p => p.Book)
                .ToList();
            return reviews;
        }
        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }
        public void DeleteReview(int reviewid)
        {
            var review = GetReviewById(reviewid);
            if (review != null)
            {
                _context.Remove(review);
                _context.SaveChanges();
            }
        }
        public Review? GetReviewById(int id)
        {
            return _context.Reviews
                .Include(p => p.User)
                .Include(p => p.Book)
                .FirstOrDefault(p => p.Id == id);
        }
        public void ChangeIsApproved(int reviewid, bool isApproved)
        {
            var review = GetReviewById(reviewid);
            if (review != null)
            {
                review.IsApproved = isApproved;
                _context.SaveChanges();
            }
        }
        public void ChangeComment(int reviewid, string newcomment)
        {
            var review = GetReviewById(reviewid);
            if (review != null)
            {
                review.Comment = newcomment;
                _context.SaveChanges();
            }
        }
        public void ChangeRating(int reviewid, float newrating)
        {
            var review = GetReviewById(reviewid);
            if (review != null)
            {
                review.Rating = newrating;
                _context.SaveChanges();
            }
        }
    }
}
