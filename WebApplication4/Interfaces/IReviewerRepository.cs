using WebApplication4.Models;

namespace WebApplication4.Interfaces
{
    public interface IReviewerRepository
    {
        public ICollection<Reviewer> GetReviewers();
        public Reviewer GetReviewer(int reviewerId);
        public ICollection<Review> GetReviewByReviewer(int reviewerId);
        public bool CreateReviewer(Reviewer reviewer);
        public bool UpdateReviewer(Reviewer reviewer);
        public bool DeleteReviewer(Reviewer reviewer);
        public bool ReviewerExists(int reviewerId);
        public bool Save();
        
    }
}
