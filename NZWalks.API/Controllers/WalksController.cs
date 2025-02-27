﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Net;

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

        //Create walk => POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Validation
            if (ModelState.IsValid)
            {
                //1- Map AddWalkRequestDto DTO to Walk Domain Model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateAsync(walkDomainModel);

                //Map Domain Model to DTO
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            }
            
            return BadRequest(ModelState);
        }

        //Get all Walks
        //GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber=1, [FromQuery] int pageSize=1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            //Create an exception
            throw new Exception("This is a new Exception");

            //Map Domain Model to DTO
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        //Get walk by ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to DTO and Mapping the response
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //Update Walk by ID
        [HttpPut]
        [Route("{id:Guid}")]
        //Validation
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            //Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

            //Validation (esto es lo mismo de arriba pero manualmente, arriba utilizamos el Validate customizado
            //if (ModelState.IsValid)
            //{
            //    //Map DTO to Domain Model
            //    var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            //    walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            //    if (walkDomainModel == null)
            //    {
            //        return NotFound();
            //    }

            //    //Map Domain Model to DTO
            //    return Ok(mapper.Map<WalkDto>(walkDomainModel));
            //}
            //return BadRequest(ModelState);

        }

        //Delete Walk by ID
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);

            if(deletedWalkDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }
    }
}
