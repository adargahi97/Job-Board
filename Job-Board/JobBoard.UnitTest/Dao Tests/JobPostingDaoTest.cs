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
    public class JobPostingDaoTest
    {
        Mock<ISqlWrapper> mockSqlWrapper;
        JobPostingDao sut;
        Guid jobPostingGuid;
        List<JobPosting> jobPosting;
        JobPostingRequest jobPostingRequest;

        [TestInitialize]
        public void Initialize()
        {
            mockSqlWrapper = new Mock<ISqlWrapper>();
            sut = new JobPostingDao(mockSqlWrapper.Object);
            jobPostingGuid = new Guid("4ad9f6b1-7a8d-4f1b-aad9-1cc48391f45f");
            jobPosting = new List<JobPosting>()
            {
                new JobPosting()
                {
                    Id = new Guid ("4ad9f6b1-7a8d-4f1b-aad9-1cc48391f45f"),
                    Position = "Test Postition",
                    LocationId = new Guid ("2abf7110-5329-469b-93bc-0db491cadb18"),
                    Department =  "Test Dept",
                    Description = "Test Desc"
                }
            };
            jobPostingRequest = new JobPostingRequest()
            {
                Position = "Test Postition",
                LocationId = new Guid("2abf7110-5329-469b-93bc-0db491cadb18"),
                Department = "Test Dept",
                Description = "Test Desc"
            };

        }

        [TestCleanup]
        public void Cleanup()
        {
            mockSqlWrapper = null;
            sut = null;
            jobPostingGuid = new Guid();
            jobPosting = null;
            jobPostingRequest = null;
        }

        [TestMethod]
        public void CreateJobPosting_ShouldCreateEntry()
        {
            _ = sut.CreateJobPosting(jobPostingRequest);

            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "INSERT INTO JobPosting (Position, LocationId, Department, Description) " +
                "VALUES (@Position, @LocationId, @Department, @Description)"), It.IsAny<DynamicParameters>()), Times.Once);

        }

        [TestMethod]
        public void GetJobPostingById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.GetJobPostingByID(jobPostingGuid);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryFirstOrDefaultAsync<LocationRequest>(It.Is<string>(sql => sql == $"SELECT * FROM JobPosting WHERE Id = '{jobPostingGuid}'")), Times.Once);
        }


        [TestMethod]
        public void DeleteJobPostingById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.DeleteJobPostingById(jobPostingGuid);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == $"DELETE FROM JobPosting WHERE Id = '{jobPostingGuid}'")), Times.Once);
        }





 

    }
}
