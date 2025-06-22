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

    }
}
