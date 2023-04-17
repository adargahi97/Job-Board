using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Job_Board.Daos;
using Job_Board.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using Job_Board.Responses;

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


        /// <summary>Get All Location Info</summary>
        /// <returns>Location Information</returns>
        /// <response code="200">Returns All Location Information</response>
        [HttpGet]
        [Route("Location")]
        public async Task<IActionResult> GetLocation()
        {
            try
            {
                var location = await _locationDao.GetLocation();
                return Ok(location);
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Get Location Information</summary>
        /// <returns>Location Information</returns>
        /// <response code="200">Create Location Information</response>
        [HttpGet]
        [Route("Location/{id}")]
        public async Task<IActionResult> GetLocationByID([FromRoute] Guid id)
        {
            try
            {
                var location = await _locationDao.GetLocationByID(id);
                if (location == null)
                {
                    return ErrorResponses.Error404("The ID You Entered");
                }
                return Ok(location);
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }


        /// <summary>Delete Location Information</summary>
        /// <returns>Location Information</returns>
        /// <response code="201">Delete Location Information</response>
        [HttpPost]
        [Route("Location")]
        public async Task<IActionResult> CreateLocation([FromBody] LocationRequest createRequest)
        {
            try
            {
                await _locationDao.CreateLocation(createRequest);
                return StatusCode(201);
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }


        /// <summary>Delete Location Information</summary>
        /// <returns>Location Information</returns>
        /// <response code="200">Delete Location Information</response>
        [HttpDelete]
        [Route("Location/{id}")]
        public async Task<IActionResult> DeleteLocationById([FromRoute] Guid id)
        {
            try
            {
                var location = await _locationDao.GetLocationByID(id);
                if (location == null)
                {
                    return ErrorResponses.Error404("The ID You Entered");
                }

                await _locationDao.DeleteLocationById(id);
                return StatusCode(200);
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }


        /// <summary>Update Location Info By ID</summary>
        /// <returns>Location Information</returns>
        /// <response code="200">Update Location Information by ID</response>
        [HttpPatch]
        [Route("Location")]
        public async Task<IActionResult> UpdateLocationByID([FromBody] Location locationReq)
        {
            try
            {
                var location = await _locationDao.GetLocationByID(locationReq.Id);
                if (location == null)
                {
                    return ErrorResponses.Error404("The ID You Entered");

                }
                

                var updatedLocation = await _locationDao.UpdateLocationById(locationReq);

                return StatusCode(200);
            }
            
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }





        
    }
}