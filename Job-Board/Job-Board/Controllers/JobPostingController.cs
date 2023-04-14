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
    public class JobPostingController : ControllerBase
    {

        private IJobPostingDao _jobPostingDao;

        public JobPostingController(IJobPostingDao jobPostingDao)
        {
            _jobPostingDao = jobPostingDao;
        }

        /// <summary>Get All Job Posting Information</summary>
        /// <returns>Job Posting Information</returns>
        /// <response code="200">Returns the Information of all Job Postings</response>

        [HttpGet]
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

        /// <summary>Search for Job Posting Information by ID</summary>
        /// <returns>Job Posting Information</returns>
        /// <response code="200">Returns the Information by ID</response>
        [HttpGet]
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
        /// <returns>Job Posting Information</returns>
        /// <response code="200">Creates Job Posting</response>
        [HttpPost]
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

        /// <summary>Delete Job Posting By ID</summary>
        /// <returns>Delete Job Posting </returns>
        /// <response code="200">Deletes Job Posting</response>
        [HttpDelete]
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

        /// <summary>Update Job Posting Information By ID</summary>
        /// <returns>Job Posting Information</returns>
        /// <response code="200">Updates Job Posting</response>
        [HttpPatch]
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

        /// <summary>Search for Job Posting Information by Location ID</summary>
        /// <returns>Job Posting Information</returns>
        /// <response code="200">Returns the Information by Location ID</response>
        [HttpGet]
        [Route("JobPosting/LocationId/{id:guid}")]
        public async Task<IActionResult> GetJobPostingByLocationId([FromRoute] Guid id)
        {
            try
            {
                var jobPosting = await _jobPostingDao.GetJobPostingByLocationId(id);
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


        /// <summary>Search for Job Posting Information by Position</summary>
        /// <returns>Job Posting Information</returns>
        /// <response code="200">Returns the Information by Position</response>
        [HttpGet]
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
                return Ok(jobPosting);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }



    }
}
