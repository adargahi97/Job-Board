using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Board.Daos;
using Job_Board.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.WebEncoders.Testing;


namespace Job_Board.Controllers
{
    [ApiController]
    public class DailySearchController : ControllerBase
    {

        private IDailySearchDao _dailySearchDao;

        public DailySearchController(IDailySearchDao dailySearchDao)
        {
            _dailySearchDao = dailySearchDao;
        }

        /// <summary>Search for Interview Information by Date</summary>
        /// <returns>Interview Information</returns>
        /// <response code="200">Returns the Interview Information found by Date</response>
        [HttpGet]
        [Route("Interview/DateTime/{date}")]
        public async Task<IActionResult> GetInterviewsByDate([FromRoute] DateTime date)
        {
            try
            {
                IEnumerable<Interview> interview = await _dailySearchDao.GetInterviewsByDate(date);
                return Ok(interview);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        /// <summary>Get Interview Information based on Today's Date</summary>
        /// <returns>Interview Information</returns>
        /// <response code="200">Returns Interview Information for Today's Date</response>
        [HttpGet]
        [Route("Interview/Today")]
        public async Task<IActionResult> GetTodaysInterviews()
        {
            try
            {
                IEnumerable<Interview> candidates = await _dailySearchDao.GetTodaysInterviews();
                return Ok(candidates);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>Interview Info By Position</summary>
        /// <returns>Interview Information</returns>
        /// <response code="200">Returns the Information by Position</response>
        [HttpGet]
        [Route("JobPosting/Position")]
        public async Task<IActionResult> DailySearchByPosition(string position)
        {
            try
            {
                IEnumerable<JobPostingDailySearchByPosition> candidates = await _dailySearchDao.DailySearchByPosition(position);

                if (!candidates.Any())
                {
                    var allPositions = await _dailySearchDao.CheckJobPostingExists(position);
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
                return Ok(candidates);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }



    }
}