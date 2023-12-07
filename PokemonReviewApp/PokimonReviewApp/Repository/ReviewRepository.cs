using Microsoft.EntityFrameworkCore;
using PokimonReviewApp.Data;
using PokimonReviewApp.Interfaces;
using PokimonReviewApp.Models;

namespace PokimonReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _dataContext;
        public ReviewRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Review GetReview(int id)
        {
           return _dataContext.Reviews.FirstOrDefault(e=>e.Id==id) ;
        }

        public bool CreateReview(Review review)
        {
            _dataContext.Add(review);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public List<Review> GetReviews()
        {
           return _dataContext.Reviews.ToList();
        }

        public List<Review> GetReviewsOfPokemon(int pokeId)
        {
           return _dataContext.Reviews.Where(e=>e.Pokemon.Id== pokeId).ToList();
        }

        public bool ReviewExists(int id)
        {
            return _dataContext.Reviews.Any(e => e.Id == id); ;
        }
        public bool UpdateReview(Review review)
        {
            _dataContext.Update(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _dataContext.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _dataContext.RemoveRange(reviews);
            return Save();
        }
    }
}
