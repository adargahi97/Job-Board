using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Job_Board.Daos;
using Job_Board.Models;
using Job_Board.Responses;
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

        /// <summary>Get Interview by ID</summary>
        /// <remarks>Retrieve Interview Information for a specific Interview ID.</remarks>
        /// <response code="200">Returns the Information of selected Interview</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("Interview/{id:Guid}")]
        public async Task<IActionResult> GetInterviewByID([FromRoute] Guid id)
        {
            try
            {
                var interview = await _interviewDao.GetInterviewById(id);
                if (interview == null)
                {
                    return ErrorResponses.ErrorInputNotFound(id.ToString());
                }
                return SuccessResponses.GetObjectSuccessful(interview);
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Create Interview</summary>
        /// <remarks>Schedule a new Interview.</remarks>
        /// <response code="201">Creates Interview</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [Route("Interview")]
        public async Task<IActionResult> CreateInterview([FromBody] InterviewRequest createRequest)
        {
            try
            {
                await _interviewDao.CreateInterview(createRequest);
                return SuccessResponses.CreateSuccessful("Inteview");
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Delete Interview</summary>
        /// <remarks>Remove an existing Interview from the system by their Interview ID.</remarks>
        /// <response code="200">Deletes Interview</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("Interview/{id}")]
        public async Task<IActionResult> DeleteInterviewById([FromRoute] Guid id)
        {
            try
            {
                var interview = await _interviewDao.GetInterviewById(id);
                if (interview == null)
                {
                    return ErrorResponses.ErrorInputNotFound(id.ToString());
                }

                await _interviewDao.DeleteInterviewById(id);
                return SuccessResponses.DeleteSuccessful(id.ToString());
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Update Interview</summary>
        /// <remarks>Modify an existing Interview's information by the Interview ID.</remarks>
        /// <response code="200">Updates Interview by ID</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("Interview")]
        public async Task<IActionResult> UpdateInterviewById([FromBody] Interview interviewReq)
        {
            try
            {
                var interview = await _interviewDao.GetInterviewById(interviewReq.Id);
                if (interview == null)
                {
                    return ErrorResponses.ErrorInputNotFound(interviewReq.Id.ToString());
                }
                var updatedInterview = await _interviewDao.UpdateInterviewById(interviewReq);

                return SuccessResponses.UpdateObjectSuccessful($"Interview: {interviewReq.Id.ToString()}");
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }
        /// <summary>Search Interviews by Date</summary>
        /// <remarks>Find all Interviews scheduled for a specific date.</remarks>
        /// <response code="200">Returns the Interview Information found by Date</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("Interview/Date/{date}")]
        public async Task<IActionResult> GetInterviewsByDate(DateTime date)
        {
            try
            {
                IEnumerable<InterviewJoin> interviews = await _interviewDao.GetInterviewsByDate(date);

                if (!interviews.Any())
                {
                    DateTime temp;
                    if (!DateTime.TryParse(date.ToString(), out temp))
                    {
                        return ErrorResponses.Error404(date.ToShortDateString());
                    }
                    return ErrorResponses.ErrorNoCandidate(date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                }
                return SuccessResponses.GetAllSuccessful(interviews);
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }
        /// <summary>Get Interview by Position</summary>
        /// <remarks>Retrieve all Interviews scheduled for a specific position.</remarks>
        /// <response code="200">Returns the Information by Position</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("JobPosting/Position")]
        public async Task<IActionResult> GetInterviewsByPosition(string position)
        {
            try
            {
                IEnumerable<InterviewJoin> candidates = await _interviewDao.GetInterviewsByPosition(position);

                if (!candidates.Any())
                {
                    var allPositions = await _interviewDao.CheckJobPostingExists(position);
                    var stringListOfPositions = allPositions.Select(jp => jp.Position).ToList();

                    foreach (string j in stringListOfPositions)
                    {
                        if (j.ToLower() == position.ToLower())
                        {
                            return ErrorResponses.ErrorNoCandidate(position);
                        }
                    }

                    return ErrorResponses.Error404(position);
                }
                return SuccessResponses.GetAllSuccessful(candidates);
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }
        /// <summary>Search Interview by Job ID</summary>
        /// <remarks>Retrieve all Interviews scheduled for a specific Job ID.</remarks>
        /// <response code="200">Returns the Interview Information by Job Id</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("Interview/JobId/{id:guid}")]
        public async Task<IActionResult> GetInterviewByJobId([FromRoute] Guid id)
        {
            try
            {
                var interview = await _interviewDao.GetInterviewByJobId(id);
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
        /// <summary>Search Interview by Last Name</summary>
        /// <remarks>Retrieve all Interviews scheduled for a Candidate with a specific last name.</remarks>
        /// <response code="200">Returns the Interview Information found by last name</response>
        /// <response code="404">Data invalid</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Route("Interview/LastName/{lastName}")]
        public async Task<IActionResult> GetInterviewByLastName([FromRoute] string lastName)
        {
            try
            {
                IEnumerable<InterviewJoinCandidate> interview = await _interviewDao.GetInterviewByLastName(lastName);
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
    }
}