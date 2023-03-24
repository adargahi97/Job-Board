using System;
using System.Threading.Tasks;
using Job_Board.Daos;
using Job_Board.Models;
using Microsoft.AspNetCore.Mvc;

namespace Job_Board.Controllers
{
    [ApiController]
    public class LocationsController : ControllerBase
    {

        private ILocationDao _locationsDao;
        public LocationsController(ILocationDao locationsDao)
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

        [HttpGet]
        [Route("Locations/{id:int}")]
        public async Task<IActionResult> GetLocationByID([FromRoute] Guid id)
        {
            try
            {
                var location = await _locationsDao.GetLocationByID(id);
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

        [HttpPost]
        [Route("Locations")]
        public async Task<IActionResult> CreateLocation([FromBody] LocationRequest createRequest)
        {
            try
            {
                await _locationsDao.CreateLocation(createRequest);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("Locations/{id:int}")]
        public async Task<IActionResult> DeleteLocationById([FromRoute] Guid id)
        {
            try
            {
                var location = await _locationsDao.GetLocationByID(id);
                if (location == null)
                {
                    return StatusCode(404);
                }

                await _locationsDao.DeleteLocationById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("Locations")]
        public async Task<IActionResult> UpdateLocationByID([FromBody] Location locationsReq)
        {
            try
            {
                var location = await _locationsDao.GetLocationByID(locationsReq.Id);
                if (location == null)
                {
                    return StatusCode(404);
                }
                var updatedLocation = await _locationsDao.UpdateLocationById(locationsReq);

                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}