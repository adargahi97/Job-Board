using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Board;
using Job_Board.Daos;
using Job_Board.Models;
using Job_Board.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;

namespace Job_Board.Controllers
{
    [ApiController]
    public class JobPostingController : ControllerBase
    {

        private IJobPostingDao _jobPostingDao;

        public JobPostingController(IJobPostingDao jobPostingDao)
        {
            _jobPostingDao = jobPostingDao;
        }

        /// <summary>Get Job Posting by ID</summary>
        /// <remarks>Retrieve a Job Postings information by its Job ID.</remarks>
        /// <response code="200">Returns the Information by ID</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("JobPosting/{id}")]
        public async Task<IActionResult> GetJobPostingById([FromRoute] Guid id)
        {
            try
            {
                var jobPosting = await _jobPostingDao.GetJobPostingById(id);
                if (jobPosting == null)
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

        /// <summary>Create Job Posting</summary>
        /// <remarks>Add a new Job Posting to the system.</remarks>
        /// <response code="201">Create Job Posting</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [Route("JobPosting")]
        public async Task<IActionResult> CreateJobPosting([FromBody] JobPostingRequest createRequest)
        {
            try
            {
                await _jobPostingDao.CreateJobPosting(createRequest);
                return SuccessResponses.CreateSuccessful("Job Posting");
            }
            catch (Exception e)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Delete Job Posting</summary>
        /// <remarks>Remove an existing Job Posting from the system by its Job ID.</remarks>
        /// <response code="200">Delete Job Posting</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("JobPosting/{id}")]
        public async Task<IActionResult> DeleteJobPostingById([FromRoute] Guid id)
        {
            try
            {
                var jobPosting = await _jobPostingDao.GetJobPostingById(id);
                if (jobPosting == null)
                {
                    return ErrorResponses.ErrorInputNotFound(id.ToString());
                }

                await _jobPostingDao.DeleteJobPostingById(id);
                return SuccessResponses.DeleteSuccessful("Job Posting");
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Update Job Posting</summary>
        /// <remarks>Modify an existing Job Posting's information by its Job ID.</remarks>
        /// <response code="200">Updates Job Posting</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("JobPosting")]
        public async Task<IActionResult> UpdateJobPostingById([FromBody] JobPosting jobPostingReq)
        {
            try
            {
                var candidate = await _jobPostingDao.GetJobPostingById(jobPostingReq.Id);
                if (candidate == null)
                {
                    return ErrorResponses.ErrorUpdating(jobPostingReq.Id.ToString());
                }
                var updatedJobPosting = await _jobPostingDao.UpdateJobPostingById(jobPostingReq);

                return SuccessResponses.UpdateObjectSuccessful(jobPostingReq.Id.ToString());
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }
        /// <summary>Search Job Posting by Position</summary>
        /// <remarks>Retrieve Job Posting information for a specific Position.</remarks>
        /// <response code="200">Returns the Information by Position</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("JobPosting/Position/{position}")]
        public async Task<IActionResult> GetJobPostingByPosition([FromRoute] string position)
        {
            try
            {
                var jobPosting = await _jobPostingDao.GetJobPostingByPosition(position);
                if (jobPosting == null)
                {
                    return ErrorResponses.Error404(position);
                }
                return SuccessResponses.GetObjectSuccessful(jobPosting);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        /// <summary>Search Job Posting by Building</summary>
        /// <remarks>Retrieve Job Posting information for a specific Building.</remarks>
        /// <response code="200">Returns the Information by Position</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("JobPosting/Building/{building}")]
        public async Task<IActionResult> GetJobPostingByBuilding([FromRoute] string building)
        {
            try
            {
                var jobPosting = await _jobPostingDao.GetJobPostingByBuilding(building);
                if (!jobPosting.Any())
                {
                    return ErrorResponses.Error404(building);
                }
                return SuccessResponses.GetAllSuccessful(jobPosting);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


    }
}
