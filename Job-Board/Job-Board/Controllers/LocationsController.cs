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

        private ILocationsDao locationsDao;

        //public LocationsController(ILocationsDao locationsDao)
        //{
        //    this.locationsDao = locationsDao;
        //}

        //public void CallDao()
        //{
        //    locationsDao.GetLocations();

        //}


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
        public async Task<IActionResult> GetLocationByID([FromRoute] int id)
        {
            try
            {
                var location = await _locationsDao.GetLocationsByID(id);
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
        public async Task<IActionResult> CreateLocation([FromBody] LocationsRequest createRequest)
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
        public async Task<IActionResult> DeleteLocationById([FromRoute] int id)
        {
            try
            {
                var location = await _locationsDao.GetLocationsByID(id);
                if (location == null)
                {
                    return StatusCode(404);
                }

                await _locationsDao.DeleteLocationsById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("Locations")]
        public async Task<IActionResult> UpdateLocationByID([FromBody] Locations locationsReq)
        {
            try
            {
                var location = await _locationsDao.GetLocationsByID(locationsReq.Id);
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