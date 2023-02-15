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

        private readonly JobPostingDao _jobPostingDao;

        public JobPostingController(JobPostingDao jobPostingDao)
        {
            _jobPostingDao = jobPostingDao;
        }

        public JobPostingController()
        {
        }

        private IJobPostingDao jobPostingDao;

        public JobPostingController(IJobPostingDao jobPostingDao)
        {
            this.jobPostingDao = jobPostingDao;
        }

        //public void CallDao()
        //{
        //    jobPostingDao.GetJobPosting();

        //}


        [HttpGet]
        [Route("Job_Posting")]
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
    }
}