using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        /*Constructor*/
        public RegionsController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public IActionResult GetAllRegions() 
        {
                                                         /*STATIC List Way*/
/*            var regions = new List<Region>()
            {
                new Region
                {
                Id = Guid.NewGuid(),
                Name = "Wellington",
                Code = "WLG",
                Area = 227755,
                Lat = 1.8822,
                Long = 299.88,
                Population = 500000
                },
                new Region
            {
                Id = Guid.NewGuid(),
                Name = "Wellington",
                Code = "WLG",
                Area = 227755,
                Lat = 1.8822,
                Long = 299.88,
                Population = 500000
            }
            };*/

                                                           /*Repository Way*/
          var regions = regionRepository.GetAll();

            //return DTO regions
            var regionsDTO = new List<Models.DTO.Region>();
            regions.ToList().ForEach(region =>
            {
                var regionDTO = new Models.DTO.Region()
                { 
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    Area = region.Area,
                    Lat = region.Lat,
                    Long = region.Long,
                    Population = region.Population,
                };

                regionsDTO.Add(regionDTO);

            });


            return Ok(regions);

        }
    }
}
