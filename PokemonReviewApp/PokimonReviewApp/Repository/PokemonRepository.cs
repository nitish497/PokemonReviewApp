using Microsoft.EntityFrameworkCore;
using PokimonReviewApp.Data;
using PokimonReviewApp.Interfaces;
using PokimonReviewApp.Models;

namespace PokimonReviewApp.Repository
{
    public class PokemonRepository:IPokemonService
    {
        private readonly DataContext _dataContext;
        public PokemonRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreatePokemon(int ownerID, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity=_dataContext.Owners.Where(e=>e.Id==ownerID).FirstOrDefault();
            var pokemonCategoryEntity=_dataContext.Categories.Where(e=>e.Id==categoryId).FirstOrDefault();
            var pokemonOwner = new PokemonOwner()
            {
                Owner= pokemonOwnerEntity,
                Pokemon=pokemon,
            };

            _dataContext.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category= pokemonCategoryEntity,
                Pokemon=pokemon,
            };

            _dataContext.Add(pokemonCategory);
            _dataContext.Add(pokemon);

           return Save();
        }

        public Pokemon GetPokemon(int id)
        {
            Pokemon pokemon = _dataContext.Pokemons.FirstOrDefault(x => x.Id == id);
            return pokemon;
        }

        public Pokemon GetPokemon(string name)
        {
          return _dataContext.Pokemons.FirstOrDefault(x => x.Name==name);
        }

        public decimal GetPokemonRating(int pokId)
        {
             var reviews=_dataContext.Reviews.Where(x => x.Pokemon.Id == pokId);
            if(reviews.Count()==0)
            return 0;
            return reviews.Sum(x => x.Rating)/reviews.Count();
        }

        public List<Pokemon> GetPokemons()
        {
            return _dataContext.Pokemons.ToList();
        }

        public bool PokemonExists(int pokId)
        {
            return _dataContext.Pokemons.FirstOrDefault(x=>x.Id==pokId) != null?true:false;
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved>0?true:false;
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            _dataContext.Update(pokemon);
            return Save();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _dataContext.Remove(pokemon);
            return Save();
        }

    }
}
