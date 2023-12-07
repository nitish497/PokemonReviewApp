using PokimonReviewApp.Models;

namespace PokimonReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        List<Review> GetReviews();
        Review GetReview(int id);
        bool ReviewExists(int id);
        List<Review> GetReviewsOfPokemon(int pokeId);
        bool CreateReview(Review review);
        bool Save();
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
    }
}
