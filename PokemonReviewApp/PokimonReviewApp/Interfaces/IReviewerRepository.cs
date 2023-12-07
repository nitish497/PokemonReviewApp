using PokimonReviewApp.Models;

namespace PokimonReviewApp.Interfaces
{
    public interface IReviewerRepository
    {
        List<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);
       List<Review> GetReviewsByReviewer(int reviewerId);
        bool ReviewerExists(int id);
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool DeleteReviewer(Reviewer reviewer);
        bool Save();

    }
}
