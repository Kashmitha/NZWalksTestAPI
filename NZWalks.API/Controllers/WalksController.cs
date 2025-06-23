using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Map the DTO to the Domain Model using AutoMapper
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
            await walkRepository.CreateAsync(walkDomainModel); // Call the repository to create the walk

            //Map domain model back to DTO if needed
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto); // Return a 200 OK response
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync(); // Retrieve all walks from the repository

            // Map the list of domain models to a list of DTOs
            var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);

            return Ok(walksDto); // Return a 200 OK response with the list of walks
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id); // Retrieve a walk by its ID from the repository

            if(walkDomainModel == null)
            {
                return NotFound(); // Return a 404 Not Found response if the walk does not exist
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel)); // Map the domain model to a DTO and return it with a 200 OK response


        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map the DTO to the Domain Model using AutoMapper
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel); // Call the repository to update the walk
            if(walkDomainModel == null)
            {
                return NotFound(); // Return a 404 Not Found response if the walk does not exist
            }
            // Map the updated domain model back to a DTO
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }
    }
}
