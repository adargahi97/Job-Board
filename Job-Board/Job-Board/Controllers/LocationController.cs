using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Board.Daos;
using Job_Board.Models;
using Microsoft.AspNetCore.Mvc;

namespace Job_Board.Controllers
{
    [ApiController]
    public class LocationController : ControllerBase
    {

        private ILocationDao _locationDao;
        public LocationController(ILocationDao locationDao)
        {
            _locationDao = locationDao;
        }



        [HttpGet]
        [Route("Location")]
        public async Task<IActionResult> GetLocation()
        {
            try
            {
                var location = await _locationDao.GetLocation();
                return Ok(location);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("Location/{id}")]
        public async Task<IActionResult> GetLocationByID([FromRoute] Guid id)
        {
            try
            {
                var location = await _locationDao.GetLocationByID(id);
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
        [Route("Location")]
        public async Task<IActionResult> CreateLocation([FromBody] LocationRequest createRequest)
        {
            try
            {
                await _locationDao.CreateLocation(createRequest);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("Location/{id}")]
        public async Task<IActionResult> DeleteLocationById([FromRoute] Guid id)
        {
            try
            {
                var location = await _locationDao.GetLocationByID(id);
                if (location == null)
                {
                    return StatusCode(404);
                }

                await _locationDao.DeleteLocationById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("Location")]
        public async Task<IActionResult> UpdateLocationByID([FromBody] Location locationReq)
        {
            try
            {
                var location = await _locationDao.GetLocationByID(locationReq.Id);
                if (location == null)
                {
                    return StatusCode(404);
                }
                var updatedLocation = await _locationDao.UpdateLocationById(locationReq);

                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet]
        [Route("Location/Building/{building}")]
        public async Task<IActionResult> GetLocationByBuilding([FromRoute] string building)
        {
            try
            {
                var location = await _locationDao.GetLocationByBuilding(building);
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