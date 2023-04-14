using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Board.Daos;
using Job_Board.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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

        /// <summary>Get All Candidate's Information</summary>
        /// <returns>Candidate Information</returns>
        /// <response code="200">Returns the Information of all Candidates</response>
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

        /// <summary>Get Candidate Information by Job ID</summary>
        /// <returns>Candidate Information</returns>
        /// <response code="200">Returns the Information of selected Candidates</response>
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

        /// <summary>Create a new Candidate</summary>
        /// <returns></returns>
        /// <response code="201">Candidate has been successfully created</response>
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

        /// <summary>Delete a Candidate By ID</summary>
        /// <returns></returns>
        /// <response code="200">Candidate has been successfully deleted</response>
        [HttpDelete]
        [Route("Candidate/{id}")]
        public async Task<IActionResult> DeleteCandidateById([FromRoute] Guid id)
        {
            try
            {
                var candidate = await _candidateDao.GetCandidateByID(id);
                if (candidate == null)
                {
                    return ErrorResponses.Error404("The ID You Entered");
                }

                await _candidateDao.DeleteCandidateById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Update a Candidate by Candidate ID</summary>
        /// <returns></returns>
        /// <response code="200">Candidate has been successfully Updated</response>
        /// 
        [HttpPatch]
        [Route("Candidate")]
        public async Task<IActionResult> UpdateCandidateByID([FromBody] Candidate candidateReq)
        {
            try
            {
                var candidate = await _candidateDao.GetCandidateByID(candidateReq.Id);
                if (candidate == null)
                {
                    return ErrorResponses.Error404("The ID You Entered");
                }

                var updatedCandidate = await _candidateDao.UpdateCandidateById(candidateReq);

                return StatusCode(200);
            }
            catch (SqlException e)
            {
                return StatusCode(400, "this is a 400");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}