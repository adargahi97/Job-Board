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
    public class JobPostingDaoTest
    {
        //[TestMethod]
        //void CallSqlWithString()
        //{

        //    Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();

        //    JobPostingDao sut = new JobPostingDao(mockSqlWrapper.Object);

        //    sut.GetJobPosting();

        //    mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query<JobPosting>(It.Is<string>(sql => sql == "SELECT * FROM [DBO].[JOBBOARD]")), Times.Once);

        //}


        [TestMethod]
        public void DeleteJobPostingByID_Works()
        {

            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();

            JobPostingDao sut = new JobPostingDao(mockSqlWrapper.Object);
            Guid id = Guid.Parse("647C0678 - E977 - 4723 - A00D - 74DB0085A964");

            // Act
            _ = sut.DeleteJobPostingById(id);

            // Assert

            mockSqlWrapper.Verify(x => x.ExecuteAsync(It.Is<string>(sql => sql == $"DELETE FROM JobPosting WHERE Id = {id}")), Times.Once); ;
        }

        [TestMethod]
        public void GetJobPostingByID_NotNull()
        {

            //ARRANGE
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();

            JobPostingDao sut = new JobPostingDao(mockSqlWrapper.Object);
            Guid id = Guid.Parse("647C0678 - E977 - 4723 - A00D - 74DB0085A964");

            //ACT

            var jobPosting = sut.GetJobPostingByID(id);

            //ASSERT
            Assert.IsTrue(jobPosting.Id > 0);
            Assert.IsNotNull(jobPosting.Id);

        }


        [TestMethod]
        public void GetJobPostingByPosition_PullsInfo()
        {

            //ARRANGE
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            JobPostingDao sut = new JobPostingDao(mockSqlWrapper.Object);
            string position = "Intern";

            //ACT
            _ = sut.GetJobPostingByPosition(position);
            //Assert
            //Assert.AreEqual(candidate.FirstName, "Ron");
            mockSqlWrapper.Verify(x => x.QueryFirstOrDefaultAsync<JobPosting>(It.Is<string>(sql => sql == $"GET FROM JobPosting WHERE Postion = {position}")), Times.Once);

        }

        [TestMethod]
        public void GetJobPostingByLocationId_PullsInfo()
        {

            //ARRANGE
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            JobPostingDao sut = new JobPostingDao(mockSqlWrapper.Object);
            Guid locationId = Guid.Parse("2ABF7110-5329-469B-93BC-0DB491CADB18");

            //ACT
            _ = sut.GetJobPostingByLocationId(locationId);
            //Assert
            //Assert.AreEqual(candidate.FirstName, "Ron");
            mockSqlWrapper.Verify(x => x.QueryFirstOrDefaultAsync<JobPostingRequest>(It.Is<string>(sql => sql == $"GET FROM JobPosting WHERE LocationId = {locationId}")), Times.Once);

        }

    }
}
