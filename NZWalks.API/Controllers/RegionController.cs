using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            //var regions = new List<Region>;
            //{
            var regionsDomain = dbContext.Regions.ToList();
            //{
            //new Region
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Auckland Region",
            //    Code = "AKL",
            //    RegionImageUrl = "https://res.cloudinary.com/hello-tickets/image/upload/c_limit,f_auto,q_auto,w_1300/v1685896116/mtsftjtuheccqdywwk3n.jpg"
            //},
            //new Region
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Wellington Region",
            //    Code = "WLG",
            //    RegionImageUrl = "https://cdn.tasteatlas.com/Images/Regions/8415f5406dee4881a39dd799f541e1ac.jpg?mw=1900"
            //}
            //};
            // Map Domain Models to DTO
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                });
            }

            //Return DTO as well
            return Ok(regionsDto);
            //}
        }

        //GET Single Region by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetByID([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);

            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id); // Finds the first region with the specified Id

            if (regionDomain == null)
            {
                return NotFound(); // Returns a 404 Not Found response if the region is not found
            }

            //Convert RegionDomain to RegionDto
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            return Ok(regionDto);
        }
        // POST : https://......
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Use Domain Model to create a new Region in the database
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges(); // Save changes to the database

            //Map domain model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetByID), new { id = regionDto.Id }, regionDto); // Returns a 201 Created response with the location of the new resource
        }


        //PUT : https://....
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Check if region exits
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to Domain model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            dbContext.SaveChanges();

            //Convert Domain to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }
    }
}