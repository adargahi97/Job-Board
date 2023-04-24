using Job_Board.Controllers;
using Job_Board.Daos;
using Job_Board.Models;
using Job_Board.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.UnitTest.Controller_Tests
{
    [TestClass]
    public class LocationsControllerTest
    {

        Mock<Location> mockLocationModel;
        Mock<ILocationDao> mockLocationDao;
        LocationController sut;
        Guid locationGuid;
        Location mockLocation;
        LocationRequest locationRequest;
        IEnumerable<Location> locations;

        [TestInitialize]
        public void Initialize()
        {
            mockLocationDao = new Mock<ILocationDao>();
            sut = new LocationController(mockLocationDao.Object);
            locationGuid = new Guid("2abf7110-5329-469b-93bc-0db491cadb18");
            locations = new List<Location>() { mockLocation };
            locationRequest = new LocationRequest()
            {
                StreetAddress = "Tango",
                City = "Test City",
                State = "New Textico",
                Zip = 0,
                Building = "Testing"
            };
            mockLocation = new Location()
            {
                Id = new Guid("2abf7110-5329-469b-93bc-0db491cadb18"),
                StreetAddress = "Tango",
                City = "Test City",
                State = "New Textico",
                Zip = 0,
                Building = "Testing"
            };

        }
        [TestCleanup]
        public void Cleanup()
        {
            mockLocationDao = null;
            sut = null;
            locationGuid = new Guid();
            mockLocation = null;
            locationRequest = null;
        }


        [TestMethod]
        public async Task CreateLocation_ReturnsOk_WhenModelIsValid()
        {
            mockLocationDao
                .Setup(x => x.CreateLocation(It.IsAny<LocationRequest>()))
                .Callback(() => { return; });

            var result = await sut.CreateLocation(locationRequest);

            var okResult = result as ContentResult;

            // Assert

            Assert.IsNotNull(result);
            Assert.IsTrue(result is ContentResult);
            Assert.IsInstanceOfType(result, typeof(ContentResult));
            Assert.AreEqual("\"Location has been successfully created.\"", okResult.Content);
            Assert.AreEqual(StatusCodes.Status201Created, okResult.StatusCode);
        }

        [TestMethod]
        public async Task UpdateLocation_ReturnsOk_WhenModelIsValid()
        {
            mockLocationDao
                .Setup(x => x.GetLocationById(It.IsAny<Guid>())).Returns(Task.FromResult<LocationRequest>(locationRequest))
                .Callback(() => { return; });

            var result = await sut.UpdateLocationById(mockLocation);

            // Assert
            var okResult = result as ContentResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(result is ContentResult);
            Assert.IsInstanceOfType(result, typeof(ContentResult));
            Assert.AreEqual("\"2abf7110-5329-469b-93bc-0db491cadb18 has been successfully updated.\"", okResult.Content);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

        }

        [TestMethod]
        public async Task GetLocationById_ReturnsOk_WhenModelIsValid()
        {
            mockLocationDao
                .Setup(x => x.GetLocationById(It.IsAny<Guid>())).Returns(Task.FromResult<LocationRequest>(locationRequest));

            var result = await sut.GetLocationById(locationGuid);

            // Assert
            var okResult = result as ObjectResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task DeleteLocation_ReturnsOk_WhenModelIsValid()
        {
            mockLocationDao
                .Setup(x => x.GetLocationById(It.IsAny<Guid>())).Returns(Task.FromResult<LocationRequest>(locationRequest))
                .Callback(() => { return; });

            var result = await sut.DeleteLocationById(locationGuid);

            // Assert
            var okResult = result as ContentResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(result is ContentResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual("\"Location has been successfully deleted.\"", okResult.Content);
        }
        [TestMethod]
        public async Task GetAddressByLocation_ReturnsOk_WhenModelIsValid()
        {
            mockLocationDao
                .Setup(x => x.GetAddressByBuilding(It.IsAny<string>())).Returns(Task.FromResult(locations))
                .Callback(() => { return; });

            var result = await sut.GetAddressByLocation("Testing");


            //Assert
            var okResult = result as ContentResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result,typeof(ContentResult));
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }




    }
}
