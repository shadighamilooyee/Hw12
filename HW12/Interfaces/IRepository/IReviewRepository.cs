using HW12.Entities;

namespace HW12.Interfaces.IRepository
{
    public interface IReviewRepository
    {
        List<Review> GetAllReviews();
        void AddReview(Review review);
        void DeleteReview(int reviewid);
        Review GetReviewById(int id);
        void ChangeIsApproved(int reviewid, bool isApproved);
        void ChangeComment(int reviewid, string newcomment);
        void ChangeRating(int reviewid, float newrating);
    }
}
