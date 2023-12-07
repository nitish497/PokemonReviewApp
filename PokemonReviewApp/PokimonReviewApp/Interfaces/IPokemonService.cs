using PokimonReviewApp.Models;

namespace PokimonReviewApp.Interfaces
{
    public interface IPokemonService
    {
        List<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPokemonRating(int pokId);
        bool PokemonExists(int pokId);
        bool CreatePokemon(int ownerID, int categoryId, Pokemon pokemon);
        bool Save();
        bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon);
        bool DeletePokemon(Pokemon pokemon);

    }
}
