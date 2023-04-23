using Job_Board.Controllers;
using Job_Board.Daos;
using Job_Board.Models;
using Job_Board.Wrappers;
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


        Mock<ILocationDao> mockLocationDao;
        LocationController sut;
        Guid locationGuid;
        List<Job_Board.Models.Location> location;
        LocationRequest locationRequest;

        [TestInitialize]
        public void Initialize()
        {
            mockLocationDao = new Mock<ILocationDao>();
            sut = new LocationController(mockLocationDao.Object);
            locationGuid = new Guid("2abf7110-5329-469b-93bc-0db491cadb18");
            location = new List<Job_Board.Models.Location>()
            {
                new Job_Board.Models.Location()
                {
                    Id = new Guid("2abf7110-5329-469b-93bc-0db491cadb18"),
                    StreetAddress = "Tango",
                    City = "Test City",
                    State = "New Textico",
                    Zip = 0,
                    Building = "Testing"
                }
            };
            locationRequest = new LocationRequest()
            {
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
            location = null;
            locationRequest = null;
        }


        [TestMethod]
        public async Task CreateLocation_ReturnsOk_WhenModelIsValid()
        {
            mockLocationDao
                .Setup(x => x.CreateLocation(It.IsAny<LocationRequest>()))
                .Callback(() => { return; });

            var result = await sut.CreateLocation(locationRequest);

            // Assert
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);
            //Assert.IsTrue(result is OkObjectResult);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task UpdateLocation_ReturnsOk_WhenModelIsValid()
        {
            mockLocationDao
                .Setup(x => x.UpdateLocationById(It.IsAny<Job_Board.Models.Location>()))
                .Callback(() => { return; });

            var result = await sut.UpdateLocationById(location.First());

            // Assert
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);
            //Assert.IsTrue(result is OkObjectResult);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));

        }

        [TestMethod]
        public async Task GetLocation_ReturnsOk_WhenModelIsValid()
        {
            mockLocationDao
                .Setup(x => x.GetLocationById(locationGuid))
                .ReturnsAsync(locationRequest);

            var result = await sut.GetLocationById(locationGuid);

            // Assert
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task DeleteLocation_ReturnsOk_WhenModelIsValid()
        {
            mockLocationDao
                .Setup(x => x.DeleteLocationById(locationGuid))
                .Callback(() => { return; });

            var result = await sut.DeleteLocationById(locationGuid);

            // Assert
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);
            //Assert.IsTrue(result is OkObjectResult);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }




    }

    //{
    //    [TestMethod]
    //    public void CallDao()
    //    {
    //        Mock<ILocationsDao> mockLocationsDao = new Mock<ILocationsDao>();

    //        LocationsController sut = new LocationsController(mockLocationsDao.Object);

    //        //sut.CallDao();

    //        mockLocationsDao.Verify(locationsDao => locationsDao.GetLocations(), Times.Once);
    //    }

    //}
}
