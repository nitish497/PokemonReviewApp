using Microsoft.AspNetCore.Mvc;
using PokimonReviewApp.Interfaces;
using PokimonReviewApp.Models;
using PokimonReviewApp.Repository;

namespace PokimonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonService _pokemonService;
        private readonly IReviewRepository _reviewRepository;
        public PokemonController(IPokemonService pokemonService, IReviewRepository reviewRepository)
        {
            _pokemonService = pokemonService;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _pokemonService.GetPokemons();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemons);
        }

        [HttpGet("{pokId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(404)]
        public IActionResult GetPokemon(int pokId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pokemon = _pokemonService.GetPokemon(pokId);

            if(pokemon == null)
            {
                return NotFound();
            }
            return Ok(pokemon);
        }

        [HttpGet("{pokId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(404)]
        public IActionResult GetPokemonRating(int pokId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rating = _pokemonService.GetPokemonRating(pokId);

            if (rating == 0)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int catId, [FromBody] Pokemon pokemonCreate)
        {
            if (pokemonCreate == null)
                return BadRequest(ModelState);

            var pokemons = _pokemonService.GetPokemons().Where(e=>e.Name.Trim().ToUpper()==pokemonCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (pokemons != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           // var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);


            if (!_pokemonService.CreatePokemon(ownerId, catId, pokemonCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{pokeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon(int pokeId,
           [FromQuery] int ownerId, [FromQuery] int catId,
           [FromBody] Pokemon updatedPokemon)
        {
            if (updatedPokemon == null)
                return BadRequest(ModelState);

            if (pokeId != updatedPokemon.Id)
                return BadRequest(ModelState);

            if (!_pokemonService.PokemonExists(pokeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

           // var pokemonMap = _mapper.Map<Pokemon>(updatedPokemon);

            if (!_pokemonService.UpdatePokemon(ownerId, catId, updatedPokemon))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{pokeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon(int pokeId)
        {
            if (!_pokemonService.PokemonExists(pokeId))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetReviewsOfPokemon(pokeId);
            var pokemonToDelete = _pokemonService.GetPokemon(pokeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }

            if (!_pokemonService.DeletePokemon(pokemonToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
    }
}
