using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        //Instance of NZWalksDbContext class
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        //GET All Regions
        //IActionResult its the response type
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //throw new Exception("This is a custom exception");
                //Get data from DataBase using CONTROLLER(dbContext)- Domain models
                //var regionsDomain = await dbContext.Regions.ToListAsync();
                var regionsDomain = await regionRepository.GetAllAsync();

                logger.LogInformation($"Finished GetAll Regions Request with data: {JsonSerializer.Serialize(regionsDomain)}");

                //Map Domain Models using AUTOMAPPER
                var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

                //Return DTOs
                return Ok(regionsDto);
                //Or more clean
                //return Ok(mapper.Map< List<RegionDto>> (regionsDomain));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
        }

        //GET Region by ID
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //2 Options to get the Regions Domain Model from DB

            //Option 1: var region = dbContext.Regions.Find(id);

            //Option 2 using LINQ and the Controller (dbContext)
            //var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            //Here we use the Repository(Interface)
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map Region Domain Model to RegionDTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl,
            //};

            //Map Region Domain Model to RegionDTO using AUTOMAPPER in the return response
            //Return DTO Back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        //POST To create a new Region
        [HttpPost]
        //Validation from CustomActionFilters (esta es una forma generica de hacerlo, dejare la forma manual de como hacerlo en el metodo Update debajo para tenerlo como ejemplo)
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or convert DTO to Domain Model (obtienes la info del json y la asignas a la clase)
            //var regionDomainModel = new Region
            //{
            //    Code = addRegionRequestDto.Code,
            //    Name = addRegionRequestDto.Name,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            //};

                //Map DTO to Domain model using AUTOMAPPER
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                //Use Domain Model to create a Region (guardas la info del json)
                //await dbContext.Regions.AddAsync(regionDomainModel);
                //await dbContext.SaveChangesAsync();

                //instead of use the controller we will use the Repository(Interface)
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                //Map Domain Model back to DTO (finalmente esa info del json la asignas al DTO para devolver la info al usuario final)
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,
                //};

                //Map Domain Model back to DTO using AUTOMAPPER
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                //CreatedAtAction= good practice, returns a 201 Code which means POST successful
                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            
        }

        //Update Region PUT
        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map DTO to Domain Model
            //var regionDomainModel = new Region
            //{
            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl,
            //};

            //Validation
            if (ModelState.IsValid)
            {
                //Map DTO to Domain Model using AUTOMAPPER
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                //This was before the Repositories implementation
                //first we check if that region exists but know we are checking this in the REPOSITORY
                //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);

                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                // Convert Domain model to DTO
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,
                //};

                // Convert Domain model to DTO using AUTOMAPPER
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                //Never pass Domain to the client, only DTOs
                return Ok(regionDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        //Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer, Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //We dont use the dbContext we use Repository
            //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);

            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //THIS IS HAPPENING IN THE REPOSITORY NOW -- Delete region (async method doesnt have a RemoveAsync method)
            //dbContext.Regions.Remove(regionDomainModel);
            //await dbContext.SaveChangesAsync();

            //return Deleted Region back
            //map Domain model to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl,
            //};

            //return Deleted Region back
            //map Domain model to DTO using AUTOMAPPER
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }

    }
}
