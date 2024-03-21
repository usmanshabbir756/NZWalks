using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Dto;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// GET: https://localhost:portnumber/api/regions
        /// </summary>
        /// <returns>
        /// list of all the regions
        /// </returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            //domain model data from database
            var regions = dbContext.Regions.ToList();
            //domain to dto map
            var regionsDto=new List<RegionDto>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    ImageUrl = region.ImageUrl,
                });
            }
            //send dto data
            return Ok(regionsDto);
        }



        /// <summary>
        /// GET:  https://localhost:portnumber/api/regions/id
        /// </summary>
        /// <returns>
        /// regions data with that id
        /// </returns>
        [HttpGet]
        [Route("{id:GUID}")]
        public IActionResult GetById([FromRoute]Guid id)
        {
            //get region domain model from database
             var region = dbContext.Regions.Find(id);
            //for not primarykey find
            //var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            //var region = dbContext.Regions.FirstOrDefault(x => x.Code == code);
            if(region == null)
            {
                return NotFound();
            }
            //map region domain to region dto
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                ImageUrl = region.ImageUrl,
            };
            return Ok(regionDto);
        }


        /// <summary>
        /// POST:  https://localhost:portnumber/api/regions
        /// </summary>
        /// <returns>
        /// list of all the regions
        /// </returns>
        
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionDto addRegionDto)
        {
            var regionDomain = new Region
            {
                Code = addRegionDto.Code,
                Name = addRegionDto.Name,
                ImageUrl = addRegionDto.ImageUrl,
            };
            dbContext.Regions.Add(regionDomain);
            dbContext.SaveChanges();
            //map domain to dto
            var regionDto=new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                ImageUrl =regionDomain.ImageUrl,
            };
            return CreatedAtAction(nameof(GetById),new {id = regionDto.Id},regionDto);
        }

        /// <summary>
        /// PUT:  https://localhost:portnumber/api/regions
        /// </summary>
        /// <returns>
        /// Update existing region
        /// </returns>
        [HttpPut]
        public IActionResult Update(Guid id,UpdateRegionDto updateRegionDto)
        {
            var regionDomainModel=dbContext.Regions.Find(id);
            //check exist or not
            if(regionDomainModel == null) { return NotFound();}
            //update values
            regionDomainModel.Code = updateRegionDto.Code;
            regionDomainModel.Name = updateRegionDto.Name;
            regionDomainModel.ImageUrl = updateRegionDto.ImageUrl;
            dbContext.SaveChanges();
            //map domain to dto
            var updatedRegionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                ImageUrl = regionDomainModel.ImageUrl,
            };
            return Ok(updatedRegionDto); 
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute]Guid id)
        {
            var redionDomainModel = dbContext.Regions.Find(id);
            if (redionDomainModel == null) { return NotFound(); }
            //delete
            dbContext.Regions.Remove(redionDomainModel);
            dbContext.SaveChanges();
            //map domain to dto
            var regionDto = new RegionDto
            {
                Id = redionDomainModel.Id,
                Name = redionDomainModel.Name,
                Code = redionDomainModel.Code,
                ImageUrl = redionDomainModel.ImageUrl,
            };
            return Ok(regionDto);
        }

    }
}
