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

        private IInterviewDao _interviewDao;

        public InterviewController(IInterviewDao interviewDao)
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

        [HttpGet]
        [Route("Interview/{id:int}")]
        public async Task<IActionResult> GetInterviewByID([FromRoute] int id)
        {
            try
            {
                var interview = await _interviewDao.GetInterviewByID(id);
                if (interview == null)
                {
                    return StatusCode(404);
                }
                return Ok(interview);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("Interview")]
        public async Task<IActionResult> CreateInterview([FromBody] InterviewRequest createRequest)
        {
            try
            {
                await _interviewDao.CreateInterview(createRequest);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("Interview/{id:int}")]
        public async Task<IActionResult> DeleteInterviewById([FromRoute] int id)
        {
            try
            {
                var interview = await _interviewDao.GetInterviewByID(id);
                if (interview == null)
                {
                    return StatusCode(404);
                }

                await _interviewDao.DeleteInterviewById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("Interview")]
        public async Task<IActionResult> UpdateInterviewByID([FromBody] Interview interviewReq)
        {
            try
            {
                var interview = await _interviewDao.GetInterviewByID(interviewReq.Id);
                if (interview == null)
                {
                    return StatusCode(404);
                }
                var updatedInterview = await _interviewDao.UpdateInterviewById(interviewReq);

                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("Interview/Job_Id/{id:int}")]
        public async Task<IActionResult> GetInterviewByJob_Id([FromRoute] int id)
        {
            try
            {
                var interview = await _interviewDao.GetInterviewByJob_Id(id);
                if (interview == null)
                {
                    return StatusCode(404);
                }
                return Ok(interview);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}