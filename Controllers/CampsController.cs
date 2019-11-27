using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowReact")]
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository campRepository;
        private readonly IMapper mapper;

        public CampsController(ICampRepository campRepository, IMapper mapper)
        {
            this.campRepository = campRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CampModels>> Post(CampModels model)
        {
            try
            {
                var monikerExis = await campRepository.GetCampAsync(model.Moniker);
                if (monikerExis != null)
                {
                    return BadRequest("huehuehuehue");
                }
                var camp = mapper.Map<Camp>(model);
                campRepository.Add(camp);
                if (await campRepository.SaveChangesAsync())
                {
                    return Created($"/api/camps/{model.Moniker}", mapper.Map<CampModels>(camp));
                }
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Oof");
            }
            return BadRequest();
        }

        [HttpPut("{moniker}")]
        public async Task<ActionResult<CampModels>> Put(string moniker, CampModels models)
        {
            try
            {
                var OldCamp = await campRepository.GetCampAsync(moniker);
                if (OldCamp == null)
                {
                    return NotFound($"{moniker} cant find this sh*t");
                }
                mapper.Map(models, OldCamp);

                if (await campRepository.SaveChangesAsync())
                {
                    return mapper.Map<CampModels>(OldCamp);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Oof");
            }
            return BadRequest();
        }

        [HttpDelete("{moniker}")]
        public async Task<ActionResult<CampModels>> Post(string moniker)
        {
            try
            {
                var monikerExis = await campRepository.GetCampAsync(moniker);
                if (monikerExis == null)
                {
                    return NotFound($"{moniker} cant find this sh*t");
                }

                campRepository.Delete(monikerExis);
                if (await campRepository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Oof");
            }
            return BadRequest("tsk");
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