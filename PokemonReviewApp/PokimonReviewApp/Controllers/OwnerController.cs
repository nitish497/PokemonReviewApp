using Microsoft.AspNetCore.Mvc;
using PokimonReviewApp.Interfaces;
using PokimonReviewApp.Models;
using PokimonReviewApp.Repository;
using System.Collections.Generic;

namespace PokimonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICountryRepository _countryRepository;
        public OwnerController(IOwnerRepository ownerRepository, ICountryRepository countryRepository)
        {
               _ownerRepository= ownerRepository;   
            _countryRepository= countryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Owner>))]
        public IActionResult GetOwners()
        {
            var owners=_ownerRepository.GetOwners();
            if(owners == null)
            {
                return BadRequest();
            }
            return Ok(owners);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200,Type=typeof(Owner))]
        public IActionResult GetOwner(int ownerId)
        {
            var owner = _ownerRepository.GetOwner(ownerId);
            
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(owner);
        }

        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(404)]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            var pokemon = _ownerRepository.GetPokemonByOwner(ownerId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (pokemon == null)
            {
                return NotFound();
            }

            return Ok(pokemon);
        }

        [HttpPut("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner(int ownerId, [FromBody] Owner updatedOwner)
        {
            if (updatedOwner == null)
                return BadRequest(ModelState);

            if (ownerId != updatedOwner.Id)
                return BadRequest(ModelState);

            if (!_ownerRepository.OwnerExist(ownerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            //var ownerMap = _mapper.Map<Owner>(updatedOwner);

            if (!_ownerRepository.UpdateOwner(updatedOwner))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] Owner ownerCreate)
        {
            if (ownerCreate == null)
                return BadRequest(ModelState);

            var owners = _ownerRepository.GetOwners()
                .Where(c => c.LastName.Trim().ToUpper() == ownerCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (owners != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // var ownerMap = _mapper.Map<Owner>(ownerCreate);

            ownerCreate.Country = _countryRepository.GetCountry(countryId);

            if (!_ownerRepository.CreateOwner(ownerCreate))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExist(ownerId))
            {
                return NotFound();
            }

            var ownerToDelete = _ownerRepository.GetOwner(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_ownerRepository.DeleteOwner(ownerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }

    }
}
