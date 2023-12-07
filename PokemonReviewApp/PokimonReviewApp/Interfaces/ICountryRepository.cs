using PokimonReviewApp.Models;

namespace PokimonReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        List<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int ownerId);
        List<Owner> GetOwnersByCountry(int countryId);
        bool CountryExists(int countryId);
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool DeleteCountry(Country country);
        bool Save();

    }
}
