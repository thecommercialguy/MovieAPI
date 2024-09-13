using WebApplication4.Data;
using WebApplication4.Interfaces;
using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly LiteDbContext _context;

        public ReviewRepository(LiteDbContext context)
        {
            _context = context;
        }

        public bool CreateReview(Review review)
        {
            _context.Reviews.Add(review);

            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);

            return Save();
        }

        public Review GetReview(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        /*public ICollection<Review> GetReviewsByMovie(int reviewId)
        {
            throw new NotImplementedException();
        }*/

        public bool ReviewExits(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);

        }

        public bool Save()
        {
            var save = _context.SaveChanges();

            return save > 1 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);

            return Save();
        }
    }
}
