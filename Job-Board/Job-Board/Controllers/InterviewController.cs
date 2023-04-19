using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Board.Daos;
using Job_Board.Models;
using Job_Board.Responses;
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

        /// <summary>Get All Interviews</summary>
        /// <remarks>Retrieve all Interviews stored in the system.</remarks>
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
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Get Interview by ID</summary>
        /// <remarks>Retrieve Interview Information for a specific Interview ID.</remarks>
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
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Create Interview</summary>
        /// <remarks>Schedule a new Interview.</remarks>
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
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Delete Interview</summary>
        /// <remarks>Remove an existing Interview from the system by their Interview ID.</remarks>
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
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Update Interview</summary>
        /// <remarks>Modify an existing Interview's information by the Interview ID.</remarks>
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
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }
    }
}