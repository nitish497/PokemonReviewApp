using PokimonReviewApp.Models;

namespace PokimonReviewApp.Interfaces
{
    public interface IOwnerRepository
    {
        List<Owner> GetOwners();
        Owner GetOwner(int ownerId);
        Owner GetOwnerOfPokemon(int pokeId);

        List<Pokemon> GetPokemonByOwner(int ownerId);
        bool OwnerExist(int ownerId);
        bool UpdateOwner(Owner owner);
        bool CreateOwner(Owner owner);
        bool DeleteOwner(Owner ownerId);
    }
}
