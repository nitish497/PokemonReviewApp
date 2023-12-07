using Microsoft.EntityFrameworkCore;
using PokimonReviewApp.Data;
using PokimonReviewApp.Interfaces;
using PokimonReviewApp.Models;

namespace PokimonReviewApp.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _dataContext;
        public ReviewerRepository(DataContext dataContext)
        {
            _dataContext= dataContext;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _dataContext.Add(reviewer);
            return Save();

        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _dataContext.Remove(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int id)
        {
           return _dataContext.Reviewers.FirstOrDefault(x=>x.Id==id);
        }

        public List<Reviewer> GetReviewers()
        {
            return _dataContext.Reviewers.ToList();
        }

        public List<Review> GetReviewsByReviewer(int reviewerId)
        {
           return _dataContext.Reviews.Where(e=>e.Reviewer.Id==reviewerId).ToList();
        }

        public bool ReviewerExists(int id)
        {
           return _dataContext.Reviewers.Any(e=>e.Id==id);
        }

        public bool Save()
        {
            var saved= _dataContext.SaveChanges();
            return saved>0?true:false ;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _dataContext.Update(reviewer);
            return Save();
        }
    }
}
