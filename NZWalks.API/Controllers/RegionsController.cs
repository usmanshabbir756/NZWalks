using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
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
        /// https://localhost:portnumber/api/regions
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

    }
}
