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
    public class LocationDaoTest
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
        public void DeleteLocationByID_Works()
        {

            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();

            LocationDao sut = new LocationDao(mockSqlWrapper.Object);
            Guid id = Guid.Parse("647C0678 - E977 - 4723 - A00D - 74DB0085A964");

            // Act
            _ = sut.DeleteLocationById(id);

            // Assert

            mockSqlWrapper.Verify(x => x.ExecuteAsync(It.Is<string>(sql => sql == $"DELETE FROM Location WHERE Id = {id}")), Times.Once); ;
        }


        [TestMethod]
        public void GetLocationByID_NotNull()
        {

            //ARRANGE
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();

            LocationDao sut = new LocationDao(mockSqlWrapper.Object);
            Guid id = Guid.Parse("647C0678 - E977 - 4723 - A00D - 74DB0085A964");

            //ACT

            var location = sut.GetLocationByID(id);

            //ASSERT
            Assert.IsTrue(location.Id > 0);
            Assert.IsNotNull(location.Id);

        }
    }
}
