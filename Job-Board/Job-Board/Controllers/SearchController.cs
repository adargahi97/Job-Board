using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Board.Daos;
using Job_Board.Models;
using Job_Board.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace Job_Board.Controllers
{
    [ApiController]
    public class SearchController : ControllerBase
    {

        private ISearchDao _searchDao;

        public SearchController(ISearchDao searchDao)
        {
            _searchDao = searchDao;
        }



        /// <summary>Search Job Postings by State</summary>
        /// <remarks>Retrieve all Job Postings in a specific state.</remarks>
        /// <response code="200">Returns the All Job Postings by State</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("JobPosting/State/{state}")]
        public async Task<IActionResult> GetJobPostingByState([FromRoute] string state)
        {
            try
            {
                IEnumerable<JobPostingByState> location = await _searchDao.GetJobPostingByState(state);
                if (!location.Any())
                {
                    return ErrorResponses.Error404(state);
                }
                return Ok(location);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        /// <summary>Get Candidates by Last Name</summary>
        /// <remarks>Retrieve all Candidates with a specific last name.</remarks>
        /// <response code="200">Returns the Candidates with matching last names</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("Candidate/LastName/{lastName}")]
        public async Task<IActionResult> GetCandidateByLastName(string lastName)
        {
            try
            {
                IEnumerable<CandidateByLastName> candidates = await _searchDao.GetCandidateByLastName(lastName);
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

        /// <summary>Search Candidates by Job ID</summary>
        /// <remarks>Retrieve all Candidates for a specific Job ID.</remarks>
        /// <response code="200">Returns the Candidate Information by Job Id</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("Candidate/JobId/{JobId}")]
        public async Task<IActionResult> GetCandidateByJobId(Guid JobId)
        {
            try
            {
                IEnumerable<CandidateByJobId> candidates = await _searchDao.GetCandidateByJobId(JobId);
                if (!candidates.Any())
                {
                    return ErrorResponses.Error404("The Id You Entered");
                }
                return Ok(candidates);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>Search Interview by Job ID</summary>
        /// <remarks>Retrieve all Interviews scheduled for a specific Job ID.</remarks>
        /// <response code="200">Returns the Interview Information by Job Id</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("Interview/JobId/{id:guid}")]
        public async Task<IActionResult> GetInterviewByJobId([FromRoute] Guid id)
        {
            try
            {
                var interview = await _searchDao.GetInterviewByJobId(id);
                if (!interview.Any())
                {
                    return ErrorResponses.Error404("The Id You Entered");
                }
                return Ok(interview);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>Search Interview by Last Name</summary>
        /// <remarks>Retrieve all Interviews scheduled for a Candidate with a specific last name.</remarks>
        /// <response code="200">Returns the Interview Information found by last name</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("Interview/{lastName}")]
        public async Task<IActionResult> GetInterviewByLastName([FromRoute] string lastName)
        {
            try
            {
                IEnumerable<InterviewJoinCandidate> interview = await _searchDao.GetInterviewByLastName(lastName);
                if (!interview.Any())
                {
                    return ErrorResponses.Error404(lastName);
                }
                return Ok(interview);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>Search Job Postings by Location</summary>
        /// <remarks>Retrieve all Job Postings for a specific Location ID.</remarks>
        /// <response code="200">Returns the Information by Location ID</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("JobPosting/LocationId/{id:guid}")]
        public async Task<IActionResult> GetJobPostingByLocationId([FromRoute] Guid id)
        {
            try
            {
                var jobPosting = await _searchDao.GetJobPostingByLocationId(id);
                if (!jobPosting.Any())
                {
                    return ErrorResponses.Error404("The ID You Entered");
                }
                return Ok(jobPosting);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        /// <summary>Search Job Posting by Position</summary>
        /// <remarks>Retrieve Job Posting information for a specific Position.</remarks>
        /// <response code="200">Returns the Information by Position</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("JobPosting/Position/{position}")]
        public async Task<IActionResult> GetJobPostingByPosition([FromRoute] string position)
        {
            try
            {
                var jobPosting = await _searchDao.GetJobPostingByPosition(position);
                if (jobPosting == null)
                {
                    return ErrorResponses.Error404(position);
                }
                return Ok(jobPosting);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        /// <summary>Get Location by Building</summary>
        /// <remarks>Retrieve Location information for a specific Building.</remarks>
        /// <response code="200">Returns Location Information by Building</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("Location/Building/{building}")]
        public async Task<IActionResult> GetLocationByBuilding([FromRoute] string building)
        {
            try
            {

                var location = await _searchDao.GetLocationByBuilding(building);
                if (location == null)
                {
                    return ErrorResponses.Error404(building);
                }

                return Ok(location);
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();

            }
        }
    }
}