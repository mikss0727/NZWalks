using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WalksController : Controller
	{
		private readonly IWalkRepository walkRepository;
		private readonly IMapper mapper;

		public WalksController(IWalkRepository walkRepository, IMapper mapper)
		{
			this.walkRepository = walkRepository;
			this.mapper = mapper;
		}

		[HttpGet]

		public async Task<IActionResult> GetAllWalksAsync()
		{
			//Fetch data FRom databasee - domain walks
			var walksDomain = await walkRepository.GetAllAsync();
			//convert domain walks to DTO Walks
			var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walksDomain);
			//Return response Ok result
			return Ok(walksDTO);

		}

		[HttpGet]
		[Route("{id:guid}")]
		[ActionName("GetWalksAsync")]

		public async Task<IActionResult> GetWalksAsync(Guid id)
		{
			// Get Walk Domain object from database
			var walkDomin = await walkRepository.GetAsync(id);

			// Convert domain to DTO
			var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomin);

			//Return response
			return Ok(walkDTO);
		}

		[HttpPost]
		public async Task<IActionResult> AddwalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
		{
			//Convert DTO to domain Object
			var walkDomain = new Models.Domain.Walk
			{
				Length = addWalkRequest.Length,
				Name = addWalkRequest.Name,
				RegionId = addWalkRequest.RegionId,
				WalkDifficultyId = addWalkRequest.WalkDifficultyId
			};
			//Pass Domain object to repository to persist this
			walkDomain = await walkRepository.AddAsync(walkDomain);

			//Convert the domain object back to DTO
			var walkDTO = new Models.DTO.Walk
			{
				Id = walkDomain.Id,
				Length = walkDomain.Length,
				Name = walkDomain.Name,
				RegionId = walkDomain.RegionId,
				WalkDifficultyId = walkDomain.WalkDifficultyId
			};
			// Send DTO response back to Client
			return CreatedAtAction(nameof(GetWalksAsync), new { id = walkDTO.Id }, walkDTO);
		}

		[HttpPut]
		[Route("{id:guid}")]

		public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
		{
			//Convert DTO to Domain object
			var walkDomain = new Models.Domain.Walk
			{
				Length = updateWalkRequest.Length,
				Name = updateWalkRequest.Name,
				RegionId = updateWalkRequest.RegionId,
				WalkDifficultyId = updateWalkRequest.WalkDifficultyId

			};

			//Pass details to Repository - Get Domain object in response (or null)
			walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

			//Handle Null (Not FOund)
			if (walkDomain == null)
			{
				return NotFound();
			}

			//Convert back domain to DTO
			var walkDTO = new Models.DTO.Walk
			{
				Id = walkDomain.Id,
				Length = walkDomain.Length,
				Name = walkDomain.Name,
				RegionId = walkDomain.RegionId,
				WalkDifficultyId = walkDomain.WalkDifficultyId

			};
			//Return Response
			return Ok(walkDTO);
		}

		[HttpDelete]
		[Route("{id:guid}")]

		public async Task<IActionResult> DeleteWalkAsync(Guid id)
		{ 
			//call repository to delete walk
			var walkDomain = await walkRepository.DeleteAsync(id);

			if (walkDomain == null)
			{
				return NotFound();
			}

			//convert to DTO using mapper
			var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

			return Ok(walkDTO);
		
		}
	}
}
