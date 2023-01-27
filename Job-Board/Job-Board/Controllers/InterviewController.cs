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
    public class InterviewController : ControllerBase
    {

        private readonly InterviewDao _interviewDao;
        public InterviewController(InterviewDao interviewDao)
        {
            _interviewDao = interviewDao;
        }

        [HttpGet]
        [Route("Interview")]
        public async Task<IActionResult> GetInterviews()
        {
            try
            {
                var interviews = await _interviewDao.GetInterviews();
                return Ok(interviews);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}