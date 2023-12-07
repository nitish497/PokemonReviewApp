using Microsoft.EntityFrameworkCore;
using PokimonReviewApp.Data;
using PokimonReviewApp.Interfaces;
using PokimonReviewApp.Models;

namespace PokimonReviewApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _dataContext;
        public OwnerRepository( DataContext dataContext)
        {
            _dataContext= dataContext;
        }
        public Owner GetOwner(int ownerId)
        {
            return _dataContext.Owners.FirstOrDefault(x=>x.Id==ownerId);
        }

        public Owner GetOwnerOfPokemon(int pokeId)
        {
            return _dataContext.PokemonOwners.Where(e=>e.Pokemon.Id==pokeId).Select(e=>e.Owner).FirstOrDefault();
        }

        public List<Owner> GetOwners()
        {
          return _dataContext.Owners.ToList();
        }

        public List<Pokemon> GetPokemonByOwner(int ownerId)
        {
           return _dataContext.PokemonOwners.Where(e=>e.Owner.Id==ownerId).Select(e=>e.Pokemon).ToList();
        }

        public bool OwnerExist(int ownerId)
        {
            return _dataContext.Owners.Any(e=>e.Id==ownerId);
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOwner(Owner owner)
        {
            _dataContext.Update(owner);
            return Save();
        }

        public bool CreateOwner(Owner owner)
        {
            _dataContext.Add(owner);
            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _dataContext.Remove(owner);
            return Save();
        }


    }
}
