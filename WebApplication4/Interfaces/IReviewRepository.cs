using WebApplication4.Models;

namespace WebApplication4.Interfaces
{
    public interface IReviewRepository
    {
        public ICollection<Review> GetReviews();
        public Review GetReview(int reviewId);
        // public ICollection<Review> GetReviewsByMovie(int reviewId);  // Reviews from a movie
        public bool CreateReview(Review review);
        public bool UpdateReview(Review review);
        public bool DeleteReview(Review review);
        public bool ReviewExits(int reviewId);
        public bool Save();
    };
}
