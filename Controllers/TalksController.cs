using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    public class TalksController : ControllerBase
    {
        private readonly ICampRepository campRepository;
        private readonly IMapper mapper;

        public TalksController(ICampRepository campRepository, IMapper mapper)
        {
            this.campRepository = campRepository;
            this.mapper = mapper;
        }

        [HttpGet("talk")]
        public async Task<ActionResult<TalkModels>> GetTalkByMoniker(string moniker, int talkId, bool includeSpeakers = false)
        {
            try
            {
                var Talker = await campRepository.GetTalkByMonikerAsync(moniker, talkId, includeSpeakers);

                return Ok(mapper.Map<TalkModels>(Talker));
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Database Failure");
            }
        }

        [HttpGet("talks")]
        public async Task<ActionResult<TalkModels[]>> GetTalksByMoniker(string moniker, bool includeSpeakers = false)
        {
            try
            {
                var Talker = await campRepository.GetTalksByMonikerAsync(moniker, includeSpeakers);

                return Ok(mapper.Map<TalkModels[]>(Talker));
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Database Failure");
            }
        }
    }
}