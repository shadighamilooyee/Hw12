using HW12.Entities;

namespace HW12.Interfaces.IRepository
{
    public interface IReviewRepository
    {
        List<Review> GetAllReviews();
        void AddReview(Review review);
        void DeleteReview(int id);
        Review GetReviewById(int id);
        void ChangeIsApproved(Review review, bool isApproved);
        void ChangeComment(Review review, string newcomment);
        void ChangeRating(Review review, float newrating);
    }
}
