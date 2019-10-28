using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository campRepository;
        private readonly IMapper mapper;

        public CampsController(ICampRepository campRepository, IMapper mapper)
        {
            this.campRepository = campRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCamp(bool includeTalks = false)
        {
            try
            {
                var ListCapms = await campRepository.GetAllCampsAsync(includeTalks);
                CampModels[] campModels = mapper.Map<CampModels[]>(ListCapms);

                return Ok(campModels);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Database Failure");
            }
           
        }

        [HttpGet("{moniker}")]
        public async Task<IActionResult> GetByMoniker(string moniker)
        {
            try
            {
                var result = await campRepository.GetCampAsync(moniker);

                return Ok(result);  
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Database Failure");
            }
        }

        //[Route("api/[controller]/GetDateCamps{date}")]
        [HttpGet("search")]
        public async Task<IActionResult> GetDateCamps(DateTime date)
        {
            try
            {
                var results = await campRepository.GetAllCampsByEventDate(date);
                if (!results.Any())
                {
                    return NotFound();
                }

                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Database Failure");
            }
        }
    }
}
