using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Board.Daos;
using Job_Board.Models;
using Microsoft.AspNetCore.Mvc;

namespace Job_Board.Controllers
{
    [ApiController]
    public class SearchController : ControllerBase
    {
        private ISearchDao _searchDao;
        public SearchController(ISearchDao searchDao)
        {
            _searchDao = searchDao;
        }

        /// <summary>Search for Location Information by State</summary>
        /// <returns>Location Information</returns>
        /// <response code="200">Returns the Information by State</response>
        [HttpGet]
        [Route("Location/State/{state}")]
        public async Task<IActionResult> GetLocationByState([FromRoute] string state)
        {
            try
            {
                IEnumerable<LocationByState> location = await _searchDao.GetLocationByState(state);
                if (location == null)
                {
                    return StatusCode(404);
                }
                return Ok(location);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
