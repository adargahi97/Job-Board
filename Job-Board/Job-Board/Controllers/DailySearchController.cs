using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Board.Daos;
using Job_Board.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Globalization;
using Job_Board.Responses;

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
        public async Task<IActionResult> GetInterviewsByDate(DateTime date)
        {
            try
            {
                IEnumerable<Interview> interviews = await _dailySearchDao.GetInterviewsByDate(date);

                if (!interviews.Any())
                {
                    DateTime temp;
                    if (!DateTime.TryParse(date.ToString(), out temp))
                    {
                        return ErrorResponses.Error404(date.ToShortDateString());
                    }
                    return ErrorResponses.ErrorNoCandidate(date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                }
                return Ok(interviews);
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Get Interview Information based on Today's Date</summary>
        /// <returns>Interview Information</returns>
        /// <response code="200">Returns Interview Information for Today's Date</response>
        [HttpGet]
        [Route("Interview/TodaysInterviews")]
        public async Task<IActionResult> GetTodaysInterviews()
        {
            try
            {
                IEnumerable<InterviewDailySearch> interviews = await _dailySearchDao.GetTodaysInterviews();

                if (!interviews.Any())
                {
                    return ErrorResponses.ErrorNoCandidate(DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                }
                return Ok(interviews);
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
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
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }
    }
}