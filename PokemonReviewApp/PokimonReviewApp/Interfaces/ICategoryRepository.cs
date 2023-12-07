using PokimonReviewApp.Models;

namespace PokimonReviewApp.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> Categories();
        Category GetCategory(int id);
        List<Pokemon> GetPokemonsByCategory(int categoryId);
        bool CategoryExist(int categoryId);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
    }
}
