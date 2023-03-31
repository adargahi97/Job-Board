using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Board.Daos;
using Job_Board.Models;
using Microsoft.AspNetCore.Mvc;

namespace Job_Board.Controllers
{
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private ICandidateDao _candidateDao;

        public CandidateController(ICandidateDao candidatedao)
        {
            _candidateDao = candidatedao;
        }


        [HttpGet]
        [Route("Candidate")]
        public async Task<IActionResult> GetCandidates()
        {
            try
            {
                IEnumerable<Candidate> candidates = await _candidateDao.GetCandidates();
                return Ok(candidates);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("Candidate/{id:Guid}")]
        public async Task<IActionResult> GetCandidateByID([FromRoute] Guid id)
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
        [Route("Candidate/{id}")]
        public async Task<IActionResult> DeleteCandidateById([FromRoute] Guid id)
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
        [HttpGet]
        [Route("Candidate/LastName/")]
        public async Task<IActionResult> GetCandidateByLastName(string lastName)
        {
            try
            {
                IEnumerable<CandidateByLastName> candidates = await _candidateDao.GetCandidateByLastName(lastName);
                return Ok(candidates);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet]
        [Route("Candidate/JobId")]
        public async Task<IActionResult> GetCandidateByJobId(Guid JobId)
        {
            try
            {
                IEnumerable<CandidateByJobId> candidates = await _candidateDao.GetCandidateByJobId(JobId);
                return Ok(candidates);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}