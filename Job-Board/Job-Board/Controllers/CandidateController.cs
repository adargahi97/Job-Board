using System;
using System.Threading.Tasks;
using Job_Board;
using Job_Board.Daos;
using Job_Board.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Job_Board.Controllers
{
    [ApiController]
    public class CandidateController : ControllerBase
    {

        private readonly CandidateDao _candidateDao;
        public CandidateController(CandidateDao candidatedao)
        {
            _candidateDao = candidatedao;
        }

        //public CandidateController()
        //{
        //}

        ////private ICandidateDao candidateDao;

        ////public CandidateController(ICandidateDao candidateDao)
        ////{
        ////    this.candidateDao = candidateDao;
        ////}

        //////public void CallDao()
        //////{
        //////    candidateDao.GetCandidate();

        //////}


        [HttpGet]
        [Route("Candidate")]
        public async Task<IActionResult> GetCandidates()
        {
            try
            {
                var candidates = await _candidateDao.GetCandidates();
                return Ok(candidates);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("Candidate/{id:int}")]
        public async Task<IActionResult> GetCandidateByID([FromRoute] int id)
        {
            try
            {
                var candidate = await _candidateDao.GetCandidateByID(id);
                if (candidate == null)
                {
                    return StatusCode(404);
                }
                return Ok(candidate);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("Candidate")]
        public async Task<IActionResult> CreateCandidate([FromBody] CandidateRequest createRequest)
        {
            try
            {
                await _candidateDao.CreateCandidate(createRequest);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("Candidate/{id:int}")]
        public async Task<IActionResult> DeleteCandidateById([FromRoute] int id)
        {
            try
            {
                var candidate = await _candidateDao.GetCandidateByID(id);
                if (candidate == null)
                {
                    return StatusCode(404);
                }

                await _candidateDao.DeleteCandidateById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("Candidate")]
        public async Task<IActionResult> UpdateCandidateByID([FromBody] Candidate candidateReq)
        {
            try
            {
                var candidate = await _candidateDao.GetCandidateByID(candidateReq.Id);
                if (candidate == null)
                {
                    return StatusCode(404);
                }
                var updatedCandidate = await _candidateDao.UpdateCandidateById(candidateReq);

                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}