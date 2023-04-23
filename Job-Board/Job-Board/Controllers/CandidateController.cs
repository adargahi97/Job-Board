using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Board.Daos;
using Job_Board.Models;
using Job_Board.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

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


        /// <summary>Get Candidate by ID</summary>
        /// <remarks>Retrieve a Candidates information by their Candidate ID.</remarks>
        /// <response code="200">Returns the Information of selected Candidates</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("Candidate/{id:Guid}")]
        public async Task<IActionResult> GetCandidateByID([FromRoute] Guid id)
        {
            try
            {
                var candidate = await _candidateDao.GetCandidateById(id);
                if (candidate == null)
                {
                    return ErrorResponses.ErrorInputNotFound(id.ToString());
                }
                return SuccessResponses.GetObjectSuccessful(candidate);
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Create Candidate</summary>
        /// <remarks>Add a new Candidate to the system.</remarks>
        /// <response code="201">Candidate has been successfully created</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [Route("Candidate")]
        public async Task<IActionResult> CreateCandidate([FromBody] CandidateRequest createRequest)
        {
            try
            {
                await _candidateDao.CreateCandidate(createRequest);
                return SuccessResponses.CreateSuccessful("Candidate");
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Delete Candidate</summary>
        /// <remarks>Remove an existing Candidate from the system by their Candidate ID.</remarks>
        /// <response code="200">Candidate has been successfully deleted</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("Candidate/{id}")]
        public async Task<IActionResult> DeleteCandidateById([FromRoute] Guid id)
        {
            try
            {
                var candidate = await _candidateDao.GetCandidateById(id);
                if (candidate == null)
                {
                    return ErrorResponses.ErrorInputNotFound(id.ToString());
                }

                await _candidateDao.DeleteCandidateById(id);
                return SuccessResponses.DeleteSuccessful(id.ToString());
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Update Candidate</summary>
        /// <remarks>Modify an existing candidate's information by their Candidate ID.</remarks>
        /// <response code="200">Candidate has been successfully Updated</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("Candidate")]
        public async Task<IActionResult> UpdateCandidateByID([FromBody] Candidate candidateReq)
        {
            try
            {
                var candidate = await _candidateDao.GetCandidateById(candidateReq.Id);
                if (candidate == null)
                {
                    return ErrorResponses.ErrorUpdating(candidateReq.Id.ToString());
                }

                var updatedCandidate = await _candidateDao.UpdateCandidateById(candidateReq);

                return SuccessResponses.UpdateObjectSuccessful(candidateReq.Id.ToString());
            }

            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }
        /// <summary>Get Candidates by Last Name</summary>
        /// <remarks>Retrieve all Candidates with a specific last name.</remarks>
        /// <response code="200">Returns the Candidates with matching last names</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("Candidate/LastName/{lastName}")]
        public async Task<IActionResult> GetCandidateByLastName(string lastName)
        {
            try
            {
                IEnumerable<CandidateJoin> candidates = await _candidateDao.GetCandidateByLastName(lastName);
                if (!candidates.Any())
                {
                    return ErrorResponses.Error404(lastName);
                }
                return Ok(candidates);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        /// <summary>Get Candidates by Phone Number</summary>
        /// <remarks>Retrieve all Candidates based on Phone Number.</remarks>
        /// <response code="200">Returns the Candidates with matching phone number</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("Candidate/PhoneNumber/{phoneNumber}")]
        public async Task<IActionResult> GetCandidateByPhoneNumber(string phoneNumber)
        {
            try
            {
                IEnumerable<CandidateJoin> candidates = await _candidateDao.GetCandidateByPhoneNumber(phoneNumber);
                if (!candidates.Any())
                {
                    return ErrorResponses.Error404(phoneNumber);
                }
                return Ok(candidates);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}