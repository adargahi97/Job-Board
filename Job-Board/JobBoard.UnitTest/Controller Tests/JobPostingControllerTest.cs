using Job_Board.Controllers;
using Job_Board.Daos;
using Job_Board.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.UnitTest.Controller_Tests
{
    [TestClass]
    public class JobPostingControllerTest
    {
        Mock<IJobPostingDao> mockJobPostingDao;
        JobPostingController sut;
        Guid jobPostingGuid;
        IEnumerable<JobPosting> jobPostings;
        IEnumerable<JobPostingRequest> jobPostingRequests;
        IEnumerable<JobPostingJoin> jobPostingJoins;
        JobPosting mockJobPosting;
        JobPostingRequest mockJobPostingRequest;
        JobPostingJoin mockJobPostingJoin;

        [TestInitialize]
        public void Initialize()
        {
            mockJobPostingDao = new Mock<IJobPostingDao>();
            sut = new JobPostingController(mockJobPostingDao.Object);
            jobPostingGuid = new Guid("08EC1E6B-0180-4325-98BB-64B9DCEC5453");
            jobPostingRequests = new List<JobPostingRequest>() { mockJobPostingRequest };
            jobPostings = new List<JobPosting>() { mockJobPosting };
            jobPostingJoins = new List<JobPostingJoin>() { mockJobPostingJoin };
            mockJobPosting = new JobPosting()
            {
                Id = new Guid("08EC1E6B-0180-4325-98BB-64B9DCEC5453"),
                Position = "Intern",
                LocationId = new Guid("725DD36D-E6F1-4096-9343-0DD85218C313"),
                Department = "HR",
                Description = "HR Intern"
            };
            mockJobPostingRequest = new JobPostingRequest()
            {
                Position = "Intern",
                LocationId = new Guid("725DD36D-E6F1-4096-9343-0DD85218C313"),
                Department = "HR",
                Description = "HR Intern"
            };
            mockJobPostingJoin = new JobPostingJoin()
            {
                Id = new Guid("08EC1E6B-0180-4325-98BB-64B9DCEC5453"),
                Position = "Intern",
                Department = "HR",
                Description = "HR Intern",
                City = "Columbia",
                State = "MO",
                Building = "Jake"
            };
        
        }

        [TestMethod]
        public async Task CreateJobPosting_ReturnsOk_WhenModelIsValid()
        {
            mockJobPostingDao
                .Setup(x => x.CreateJobPosting(It.IsAny<JobPostingRequest>()))
                .Callback(() => { return; });

            var result = await sut.CreateJobPosting(mockJobPostingRequest);

            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ContentResult);
            Assert.AreEqual("\"Job Posting has been successfully created.\"", okResult.Content);
            Assert.AreEqual(StatusCodes.Status201Created, okResult.StatusCode);
        }


        [TestMethod]
        public async Task UpdateJobPosting_ReturnsOk_WhenModelIsValid()
        {
            mockJobPostingDao
                .Setup(x => x.GetJobPostingById(It.IsAny<Guid>())).Returns(Task.FromResult<JobPostingRequest>(mockJobPostingRequest))
                .Callback(() => { return; });

            var result = await sut.UpdateJobPostingById(mockJobPosting);

            // Assert
            var okResult = result as ContentResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(result is ContentResult);
            Assert.IsInstanceOfType(result, typeof(ContentResult));
            Assert.AreEqual("\"08ec1e6b-0180-4325-98bb-64b9dcec5453 has been successfully updated.\"", okResult.Content);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

        }

        [TestMethod]
        public async Task GetJobPostingById_ReturnsOk_WhenModelIsValid()
        {
            mockJobPostingDao
                .Setup(x => x.GetJobPostingById(It.IsAny<Guid>())).Returns(Task.FromResult<JobPostingRequest>(mockJobPostingRequest));

            var result = await sut.GetJobPostingById(jobPostingGuid);

            // Assert
            var okResult = result as ObjectResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task DeleteJobPosting_ReturnsOk_WhenModelIsValid()
        {
            mockJobPostingDao
                .Setup(x => x.GetJobPostingById(It.IsAny<Guid>())).Returns(Task.FromResult<JobPostingRequest>(mockJobPostingRequest))
                .Callback(() => { return; });

            var result = await sut.DeleteJobPostingById(jobPostingGuid);

            // Assert
            var okResult = result as ContentResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(result is ContentResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual("\"Job Posting has been successfully deleted.\"", okResult.Content);
        }

        [TestMethod]
        public async Task GetJobPostingByPosition_ReturnsOK_WhenModelIsValid()
        {
            mockJobPostingDao
                .Setup(x => x.GetJobPostingByPosition(It.IsAny<string>())).Returns(Task.FromResult(mockJobPostingJoin));

            var result = await sut.GetJobPostingByPosition("Intern");
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ContentResult));
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task GetJobPostingByBuilding_ReturnsOK_WhenModelIsValid()
        {
            mockJobPostingDao
                .Setup(x => x.GetJobPostingByPosition(It.IsAny<string>())).Returns(Task.FromResult(mockJobPostingJoin));

            var result = await sut.GetJobPostingByBuilding("Jake");
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ContentResult));
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }







    }
}
