using Job_Board.Daos;
using Job_Board.Models;
using Job_Board.Wrappers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.UnitTest.Dao_Tests
{
    [TestClass]
    public class LocationsDaoTest
    {
        //[TestMethod]

        //void CallSqlWithString()
        //{
        //    Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();

        //    LocationsDao sut = new LocationsDao(mockSqlWrapper.Object);

        //    sut.GetLocationsDao();

        //    mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query<Locations>(It.Is<string>(sql => sql == "SELECT * FROM [DBO].[JOBBOARD]")), Times.Once);
        //}



        [TestMethod]
        public void DeleteLocationsByID_Works()
        {

            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();

            LocationsDao sut = new LocationsDao(mockSqlWrapper.Object);
            int id = 1;

            // Act
            _ = sut.DeleteLocationsById(id);

            // Assert

            mockSqlWrapper.Verify(x => x.ExecuteAsync(It.Is<string>(sql => sql == $"DELETE FROM Locations WHERE Id = {id}")), Times.Once); ;
        }


        [TestMethod]
        public void GetLocationsByID_NotNull()
        {

            //ARRANGE
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();

            LocationsDao sut = new LocationsDao(mockSqlWrapper.Object);

            //ACT

            var locations = sut.GetLocationsByID(1);

            //ASSERT
            Assert.IsTrue(locations.Id > 0);
            Assert.IsNotNull(locations.Id);

        }
    }
}
