using System;
using System.Threading.Tasks;
using Job_Board;
using Job_Board.Daos;
using Job_Board.Models;
using Job_Board.Responses;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>Get All Job Postings</summary>
        /// <remarks>Retrieve all Job Postings stored in the system.</remarks>
        /// <response code="200">Returns the Information of all Job Postings</response>
        /// <response code="500">Internal Server Error</response>

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("JobPosting")]
        public async Task<IActionResult> GetJobPostings()
        {
            try
            {
                var jobPostings = await _jobPostingDao.GetJobPostings();
                return Ok(jobPostings);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>Get Job Posting by ID</summary>
        /// <remarks>Retrieve a Job Postings information by its Job ID.</remarks>
        /// <response code="200">Returns the Information by ID</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("JobPosting/{id}")]
        public async Task<IActionResult> GetJobPostingByID([FromRoute] Guid id)
        {
            try
            {
                var jobPosting = await _jobPostingDao.GetJobPostingByID(id);
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
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("JobPosting")]
        public async Task<IActionResult> CreateJobPosting([FromBody] JobPostingRequest createRequest)
        {
            try
            {
                await _jobPostingDao.CreateJobPosting(createRequest);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>Delete Job Posting</summary>
        /// <remarks>Remove an existing Job Posting from the system by its Job ID.</remarks>
        /// <response code="200">Delete Job Posting</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("JobPosting/{id}")]
        public async Task<IActionResult> DeleteJobPostingById([FromRoute] Guid id)
        {
            try
            {
                var jobPosting = await _jobPostingDao.GetJobPostingByID(id);
                if (jobPosting == null)
                {
                    return ErrorResponses.Error404("The ID You Entered");
                }

                await _jobPostingDao.DeleteJobPostingById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>Update Job Posting</summary>
        /// <remarks>Modify an existing Job Posting's information by its Job ID.</remarks>
        /// <response code="200">Updates Job Posting</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("JobPosting")]
        public async Task<IActionResult> UpdateJobPostingByID([FromBody] JobPosting jobPostingReq)
        {
            try
            {
                var candidate = await _jobPostingDao.GetJobPostingByID(jobPostingReq.Id);
                if (candidate == null)
                {
                    return ErrorResponses.Error404("The ID You Entered");
                }
                var updatedJobPosting = await _jobPostingDao.UpdateJobPostingById(jobPostingReq);

                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
