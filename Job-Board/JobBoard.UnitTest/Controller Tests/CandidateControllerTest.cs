using Job_Board.Controllers;
using Job_Board.Daos;
using Job_Board.Models;
using Job_Board.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JobBoard.UnitTest.Controller_Tests
{
    [TestClass]
    public class CandidateControllerTests
    {
        Mock<ICandidateDao> mockCandidateDao;
        CandidateController sut;
        Guid candidateGuid;
        IEnumerable<Candidate> candidates;
        IEnumerable<CandidateRequest> candidateRequests;
        IEnumerable<CandidateJoin> candidateJoins;
        Candidate mockCandidate;
        CandidateRequest mockCandidateRequest;
        CandidateJoin mockCandidateJoin;

        [TestInitialize]
        public void Initialize()
        {
            mockCandidateDao = new Mock<ICandidateDao>();
            sut = new CandidateController(mockCandidateDao.Object);
            candidateGuid = new Guid("221D5EC3-D99A-4A47-8E74-2411328E99D2");
            candidateRequests = new List<CandidateRequest>() { mockCandidateRequest };
            candidates = new List<Candidate>() { mockCandidate };
            candidateJoins = new List<CandidateJoin>() { mockCandidateJoin };
            mockCandidate = new Candidate()
            {
                Id = new Guid("221D5EC3-D99A-4A47-8E74-2411328E99D2"),
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "6263182322",
                JobId = new Guid("F140AC1A-4941-46D5-8A4E-C1CA19E24C3F")

            };
            mockCandidateRequest = new CandidateRequest()
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "6263182322",
                JobId = new Guid("F140AC1A-4941-46D5-8A4E-C1CA19E24C3F")
            };
            mockCandidateJoin = new CandidateJoin()
            {
                Id = new Guid("221D5EC3-D99A-4A47-8E74-2411328E99D2"),
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "6263182322",
                Position = "Intern",
                Department = "Marketing"
            };
        }

        [TestMethod]
        public async Task CreateCandidate_ReturnsOk_WhenModelIsValid()
        {
            mockCandidateDao
                .Setup(x => x.CreateCandidate(It.IsAny<CandidateRequest>()))
                .Callback(() => { return; });
            var result = await sut.CreateCandidate(mockCandidateRequest);
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ContentResult);
            Assert.AreEqual("\"Candidate has been successfully created.\"", okResult.Content);
            Assert.AreEqual(StatusCodes.Status201Created, okResult.StatusCode);
        }
        [TestMethod]
        public async Task GetCandidateById_ReturnsOk_WhenModelIsValid()
        {
            mockCandidateDao
                .Setup(x => x.GetCandidateById(It.IsAny<Guid>())).Returns(Task.FromResult(mockCandidate));

            var result = await sut.GetCandidateByID(candidateGuid);
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ContentResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task DeleteCandidateById_ReturnsOk_WhenModelIsValid()
        {
            mockCandidateDao
                .Setup(x => x.GetCandidateById(It.IsAny<Guid>())).Returns(Task.FromResult(mockCandidate));

            var result = await sut.DeleteCandidateById(candidateGuid);
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ContentResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual("\"Candidate has been successfully deleted.\"", okResult.Content);
        }

        [TestMethod]
        public async Task UpdateCandidateById_ReturnsOk_WhenModelIsValid()
        {
            mockCandidateDao
                .Setup(x => x.GetCandidateById(It.IsAny<Guid>())).Returns(Task.FromResult(mockCandidate));

            var result = await sut.UpdateCandidateByID(mockCandidate);
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ContentResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

        }

        [TestMethod]
        public async Task GetCandidateByLastName_ReturnsOk_WhenModelIsValid()
        {
            mockCandidateDao
                .Setup(x => x.GetCandidateByLastName(It.IsAny<string>())).Returns(Task.FromResult(candidateJoins));

            var result = await sut.GetCandidateByLastName("Smith");
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ContentResult));
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task GetCandidateByPhoneNumber_ReturnsOk_WhenModelIsValid()
        {
            mockCandidateDao
                .Setup(x => x.GetCandidateByPhoneNumber(It.IsAny<string>())).Returns(Task.FromResult(candidateJoins));

            var result = await sut.GetCandidateByPhoneNumber("6263182322");
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ContentResult));
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }
    }
}
