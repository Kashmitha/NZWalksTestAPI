using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync(); // Fetch all regions from the repository

            // Map Domain Models to DTO
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain); // Use AutoMapper to convert the list of domain models to DTOs>>

            //Return DTO as well
            return Ok(regionsDto);
            //}
        }

        //GET Single Region by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);

            var regionDomain = await regionRepository.GetByIdAsync(id); // Finds the first region with the specified Id

            if (regionDomain == null)
            {
                return NotFound(); // Returns a 404 Not Found response if the region is not found
            }

            //Convert RegionDomain to RegionDto
            var regionDto = mapper.Map<RegionDto>(regionDomain); // Use AutoMapper to convert the domain model to DTO
            
            return Ok(regionDto);
        }

        // POST : https://......
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Convert DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto); // Use AutoMapper to convert the DTO to a domain model

            // Use Domain Model to create a new Region in the database
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //Map domain model to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel); // Convert the created domain model back to a DTO 

            return CreatedAtAction(nameof(GetByID), new { id = regionDto.Id }, regionDto); // Returns a 201 Created response with the location of the new resource
        }


        //PUT : https://....
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto); // Use AutoMapper to convert the DTO to a domain model

            //Check if region exits
            await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Convert Domain to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }
        
        
        //DELETE : https://....
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id); // Deletes the region with the specified Id
            if (regionDomainModel == null)
            {
                return NotFound(); // Returns a 404 Not Found response if the region is not found
            }

            //Map Domain Model to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel); // Use AutoMapper to convert the domain model to a DTO  
            return Ok(regionDto); // Returns a 200 OK response with the deleted region's details
        }
    }
}