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

        [HttpGet]
        [Route("JobPosting/{id}")]
        public async Task<IActionResult> GetCandidateByID([FromRoute] Guid id)
        {
            try
            {
                var jobPosting = await _jobPostingDao.GetJobPostingByID(id);
                if (jobPosting == null)
                {
                    return StatusCode(404);
                }
                return Ok(jobPosting);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("JobPosting")]
        public async Task<IActionResult> CreateCandidate([FromBody] JobPostingRequest createRequest)
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

        [HttpDelete]
        [Route("JobPosting/{id:int}")]
        public async Task<IActionResult> DeleteInterviewById([FromRoute] Guid id)
        {
            try
            {
                var jobPosting = await _jobPostingDao.GetJobPostingByID(id);
                if (jobPosting == null)
                {
                    return StatusCode(404);
                }

                await _jobPostingDao.DeleteJobPostingById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("JobPosting")]
        public async Task<IActionResult> UpdateInterviewByID([FromBody] JobPosting jobPostingReq)
        {
            try
            {
                var candidate = await _jobPostingDao.GetJobPostingByID(jobPostingReq.Id);
                if (candidate == null)
                {
                    return StatusCode(404);
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
