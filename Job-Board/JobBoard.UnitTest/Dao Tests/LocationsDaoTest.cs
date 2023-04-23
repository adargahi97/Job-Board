using Dapper;
using Job_Board.Daos;
using Job_Board.Models;
using Job_Board.Wrappers;
using Microsoft.CodeAnalysis;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.UnitTest.Dao_Tests
{
    [TestClass]
    public class LocationDaoTest
    {

        Mock<ISqlWrapper> mockSqlWrapper;
        LocationDao sut;
        Guid locationGuid;
        List<Job_Board.Models.Location> location;
        LocationRequest locationRequest;

        [TestInitialize]
        public void Initialize()
        {
            mockSqlWrapper = new Mock<ISqlWrapper>();
            sut = new LocationDao(mockSqlWrapper.Object);
            locationGuid = new Guid("2abf7110-5329-469b-93bc-0db491cadb18");
            location = new List<Job_Board.Models.Location>()
            {
                new Job_Board.Models.Location()
                {
                    Id = new Guid("2abf7110-5329-469b-93bc-0db491cadb18"),
                    StreetAddress = "Tango",
                    City = "Test City",
                    State = "New Testico",
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
            mockSqlWrapper = null;
            sut = null;
            locationGuid = new Guid();
            location = null;
            locationRequest = null;
        }

        [TestMethod]
        public void CreateLocation_ShouldCreateEntry()
        {
            _ = sut.CreateLocation(locationRequest);

            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "INSERT INTO Location (StreetAddress, City, State, Zip, Building) " +
            "VALUES (@StreetAddress, @City, @State, @Zip, @Building)"), It.IsAny<DynamicParameters>()), Times.Once);

        }

        [TestMethod]
        public void GetLocationById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.GetLocationById(locationGuid);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryFirstOrDefaultAsync<LocationRequest>(It.Is<string>(sql => sql == $"SELECT * FROM Location WHERE Id = '{locationGuid}'")), Times.Once);
        }

        [TestMethod]
        public void DeleteLocationById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.DeleteLocationByID(locationGuid);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == $"DELETE FROM Location WHERE Id = '{locationGuid}'")), Times.Once);
        }

    }

}
