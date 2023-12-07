using Microsoft.EntityFrameworkCore;
using PokimonReviewApp.Data;
using PokimonReviewApp.Interfaces;
using PokimonReviewApp.Models;

namespace PokimonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dbContext;

        public CategoryRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Category> Categories()
        {
            return _dbContext.Categories.ToList();
        }

        public bool CategoryExist(int categoryId)
        {
           return _dbContext.Categories.Any(x=>x.Id== categoryId);  
        }

        public bool DeleteCategory(Category category)
        {
            _dbContext.Remove(category);
            return Save();
        }

        public Category GetCategory(int id)
        {
            return _dbContext.Categories.FirstOrDefault(x=>x.Id== id);  
        }

        public List<Pokemon> GetPokemonsByCategory(int categoryId)
        {
            return _dbContext.PokemonCategories.Where(x=>x.CategoryId== categoryId).Select(x=>x.Pokemon).ToList();
        }

        public bool UpdateCategory(Category category)
        {
            _dbContext.Update(category);
            return Save();
        }
        
        public bool Save()
        {
            var saved=_dbContext.SaveChanges();
           return saved>0?true:false;
        }
        public bool CreateCategory(Category category)
        {
            _dbContext.Add(category);
            return Save();
        }

    }
}
