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
    public class LocationsController : ControllerBase
    {

        private readonly LocationsDao _locationsDao;
        public LocationsController(LocationsDao locationsDao)
        {
            _locationsDao = locationsDao;
        }

        [HttpGet]
        [Route("Locations")]
        public async Task<IActionResult> GetLocations()
        {
            try
            {
                var locations = await _locationsDao.GetLocations();
                return Ok(locations);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}