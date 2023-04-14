using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Board.Daos;
using Job_Board.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.WebEncoders.Testing;


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

        /// <summary>Search for Location Information by State</summary>
        /// <returns>Location Information</returns>
        /// <response code="200">Returns the Information by State</response>
        [HttpGet]
        [Route("Location/State/{state}")]
        public async Task<IActionResult> GetLocationByState([FromRoute] string state)
        {
            try
            {
                IEnumerable<LocationByState> location = await _searchDao.GetLocationByState(state);
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


        /// <summary>Search for Location Information by State</summary>
        /// <returns>Location Information</returns>
        /// <response code="200">Returns the Information by State</response>
        [HttpGet]
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





        /// <summary>Pulls Candidate(s) based on Last Name</summary>
        /// <returns>Candidate Information</returns>
        /// <response code="200">Returns the Candidates with matching last names</response>
        [HttpGet]
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

        /// <summary>Search for Candidates who are applying for a certain position</summary>
        /// <returns>Candidate Information</returns>
        /// <response code="200">Returns the Candidate Information by Job Id</response>
        [HttpGet]
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

        /// <summary>Search for Interview Information by Job Id</summary>
        /// <returns>Interview Information</returns>
        /// <response code="200">Returns the Interview Information by Job Id</response>
        [HttpGet]
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

        /// <summary>Search for Interview Information by Last Name</summary>
        /// <returns>Interview Information</returns>
        /// <response code="200">Returns the Interview Information found by last name</response>
        [HttpGet]
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

        /// <summary>Search for Job Posting Information by Location ID</summary>
        /// <returns>Job Posting Information</returns>
        /// <response code="200">Returns the Information by Location ID</response>
        [HttpGet]
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


        /// <summary>Search for Job Posting Information by Position</summary>
        /// <returns>Job Posting Information</returns>
        /// <response code="200">Returns the Information by Position</response>
        [HttpGet]
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


        /// <summary>Location Info By Building</summary>
        /// <returns>Location Information</returns>
        /// <response code="200">Returns Location Information by Building</response>
        [HttpGet]
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