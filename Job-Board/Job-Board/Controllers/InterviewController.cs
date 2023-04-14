using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Board.Daos;
using Job_Board.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>Get All Interview Information</summary>
        /// <returns>All interviews currently scheduled</returns>
        /// <response code="200">Returns the Information of All Interviews</response>
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

        /// <summary>Get Interview Information by Job ID</summary>
        /// <returns>Interview Information</returns>
        /// <response code="200">Returns the Information of selected Interview</response>
        [HttpGet]
        [Route("Interview/{id:Guid}")]
        public async Task<IActionResult> GetInterviewByID([FromRoute] Guid id)
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

        /// <summary>Create Job Interview</summary>
        /// <returns>Interview Information</returns>
        /// <response code="200">Creates Interview</response>
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

        /// <summary>Delete Interview by Interview ID</summary>
        /// <returns></returns>
        /// <response code="200">Deletes Interview</response>
        [HttpDelete]
        [Route("Interview/{id:int}")]
        public async Task<IActionResult> DeleteInterviewById([FromRoute] Guid id)
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

        /// <summary>Update Interview Information By Interview ID</summary>
        /// <returns>Interview Information</returns>
        /// <response code="200">Updates Interview by ID</response>
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
    }
}