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
    public class SpeakersController : ControllerBase
    {
        private readonly ICampRepository campRepository;
        private readonly IMapper mapper;

        public SpeakersController(ICampRepository campRepository, IMapper mapper)
        {
            this.campRepository = campRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSpeakers()
        {
            try
            {
                var SpeakerList = await campRepository.GetAllSpeakersAsync();

                if (!SpeakerList.Any())
                {
                    return NotFound();
                }

                return Ok(SpeakerList);
            }
            catch
            {
                return this.StatusCode(500, "Database Failure");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<SpeakModels>> GetSpeaker(int speakerId)
        {
            try
            {
                var Speaker = await campRepository.GetSpeakerAsync(speakerId);

                return mapper.Map<SpeakModels>(Speaker);
            }
            catch
            {
                return this.StatusCode(500, "Database Failure");
            }
        }


        [HttpGet("byMoniker")]
        public async Task<ActionResult<SpeakModels[]>> GetSpeakersByMoniker(string moniker)
        {
            try
            {
                var Speaker = await campRepository.GetSpeakersByMonikerAsync(moniker);

                return mapper.Map<SpeakModels[]>(Speaker);
            }
            catch
            {
                return this.StatusCode(500, "Database Failure");
            }
        }
    }
}
