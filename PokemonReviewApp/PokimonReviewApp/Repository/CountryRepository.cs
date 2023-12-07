using Microsoft.EntityFrameworkCore;
using PokimonReviewApp.Data;
using PokimonReviewApp.Interfaces;
using PokimonReviewApp.Models;

namespace PokimonReviewApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _dataContext;
        public CountryRepository(DataContext dataContext)
        {
            _dataContext= dataContext;
        }
        public bool CountryExists(int countryId)
        {
           return _dataContext.Countries.Any(x=>x.Id== countryId);
        }

        public bool CreateCountry(Country country)
        {
            _dataContext.Add(country);
            return Save(); 
        }

        public bool DeleteCountry(Country country)
        {
            _dataContext.Remove(country);
            return Save();
        }

        public List<Country> GetCountries()
        {
            return _dataContext.Countries.ToList();
        }

        public Country GetCountry(int id)
        {
            return _dataContext.Countries.FirstOrDefault(x=>x.Id==id);
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return _dataContext.Owners.Where(x=>x.Id==ownerId).Select(e=>e.Country).FirstOrDefault();
        }

        public List<Owner> GetOwnersByCountry(int countryId)
        {
            return _dataContext.Owners.Where(e => e.Country.Id == countryId).ToList(); ;
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0;
        }

        public bool UpdateCountry(Country country)
        {
            _dataContext.Update(country);
            return Save();
        }
    }
}
