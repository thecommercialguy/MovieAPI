using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Interfaces;
using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly LiteDbContext _context;

        public ReviewerRepository(LiteDbContext context)
        {
            _context = context;
        }
        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);

            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);

            return Save();
        }

        public ICollection<Review> GetReviewByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList(); // "Reviews" will return a review

        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            var exists = _context.Reviewers.Any(r => r.Id == reviewerId);

            return exists;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);

            return Save();
        }
    }
}
