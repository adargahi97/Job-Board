using Job_Board.Controllers;
using Job_Board.Daos;
using Job_Board.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JobBoard.UnitTest.Controller_Tests
{
    [TestClass]
    public class InterviewControllerTest
    {
        Mock<Interview> mockInterviewModel;
        Mock<IInterviewDao> mockInterviewDao;
        InterviewController sut;
        Guid interviewGuid;
        IEnumerable<Interview> interviews;
        IEnumerable<InterviewJoin> interviewJoinList;
        IEnumerable<InterviewRequest> interviewReqList;
        IEnumerable<InterviewJoinCandidate> interviewJoinCanList;
        Interview mockInterview;
        InterviewRequest mockInterviewRequest;
        InterviewJoinCandidate mockInterviewJoinCandidate;
        InterviewJoin mockInterviewJoin;
        DateTime mockDateTime;

        [TestInitialize]
        public void Initialize()
        {
            mockInterviewDao = new Mock<IInterviewDao>();
            sut = new InterviewController(mockInterviewDao.Object);
            interviewGuid = new Guid("D4138EFE-C728-408B-88E5-0D12E681DE19");
            interviews = new List<Interview>();
            mockDateTime = new DateTime(2023-01-01);
            interviewJoinList = new List<InterviewJoin>() { mockInterviewJoin };
            interviewReqList = new List<InterviewRequest>() { mockInterviewRequest };
            interviewJoinCanList = new List<InterviewJoinCandidate>() { mockInterviewJoinCandidate };
            mockInterviewJoinCandidate = new InterviewJoinCandidate()
            {
                FirstName = "Bob",
                LastName = "Miller",
                DateTime = "2023-01-01 12:00:00",
                LocationId = new Guid("FC27A8EF-1755-4A98-BE38-B9F5BC202CD4"),
                JobId = new Guid("0AB667E2-3936-48EC-B1DA-E7614239B788")
            };
            mockInterviewRequest = new InterviewRequest()

            {
                DateTime = "2023 - 01 - 01 12:00:00",
                LocationId = new Guid("FC27A8EF-1755-4A98-BE38-B9F5BC202CD4"),
                CandidateId = new Guid("D7748F96-FB3A-4DFA-8B28-7D0B4AAB3DB7"),
                JobId = new Guid("0AB667E2-3936-48EC-B1DA-E7614239B788")
            };
            mockInterviewJoin = new InterviewJoin()
            {
                Position = "Intern",
                Department = "Marketing",
                FirstName = "Bob",
                LastName = "Miller",
                DateTime = "2023-01-01 12:00:00",
                Building = "Tango"
            };
            mockInterview = new Interview()
            {
                Id = new Guid("D4138EFE-C728-408B-88E5-0D12E681DE19"),
                DateTime = "2023 - 01 - 01 12:00:00",
                LocationId = new Guid("FC27A8EF-1755-4A98-BE38-B9F5BC202CD4"),
                CandidateId = new Guid("D7748F96-FB3A-4DFA-8B28-7D0B4AAB3DB7"),
                JobId = new Guid("0AB667E2-3936-48EC-B1DA-E7614239B788")
            };
        }

        [TestMethod]
        public async Task CreateInterview_ReturnsOk_WhenModelIsValid()
        {
            mockInterviewDao
                .Setup(x => x.CreateInterview(It.IsAny<InterviewRequest>()))
                .Callback(() => { return; });
            var result = await sut.CreateInterview(mockInterviewRequest);
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ContentResult);
            Assert.AreEqual("\"Interview has been successfully created.\"", okResult.Content);
            Assert.AreEqual(StatusCodes.Status201Created, okResult.StatusCode);
        }
        [TestMethod]
        public async Task GetInterviewById_ReturnsOk_WhenModelIsValid()
        {
            mockInterviewDao
                .Setup(x => x.GetInterviewById(It.IsAny<Guid>())).Returns(Task.FromResult<InterviewRequest>(mockInterviewRequest));

            var result = await sut.GetInterviewById(interviewGuid);
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull (result);
            Assert.IsTrue(result is ContentResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task DeleteInterviewById_ReturnsOk_WhenModelIsValid()
        {
            mockInterviewDao
                .Setup(x => x.GetInterviewById(It.IsAny<Guid>())).Returns(Task.FromResult<InterviewRequest>(mockInterviewRequest));

            var result = await sut.DeleteInterviewById(interviewGuid);
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ContentResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual("\"Interview has been successfully deleted.\"", okResult.Content);
        }

        [TestMethod]
        public async Task UpdateInterviewById_ReturnsOk_WhenModelIsValid()
        {
            mockInterviewDao
                .Setup(x => x.GetInterviewById(It.IsAny<Guid>())).Returns(Task.FromResult<InterviewRequest>(mockInterviewRequest));

            var result = await sut.UpdateInterviewById(mockInterview);
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ContentResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task GetInterviewsByDate_ReturnsOk_WhenModelIsValid()
        {
            mockInterviewDao
                .Setup(x => x.GetInterviewsByDate(It.IsAny<DateTime>())).Returns(Task.FromResult(interviewJoinList));

            var result = await sut.GetInterviewsByDate(mockDateTime);
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ContentResult));
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task GetInterviewsByPosition_ReturnsOk_WhenModelIsValid()
        {
            mockInterviewDao
                .Setup(x => x.GetInterviewsByPosition(It.IsAny<string>())).Returns(Task.FromResult(interviewJoinList));

            var result = await sut.GetInterviewsByPosition("Intern");
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ContentResult));
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task GetInterviewsByJobId_ReturnsOk_WhenModelIsValid()
        {
            mockInterviewDao
                .Setup(x => x.GetInterviewByJobId(It.IsAny<Guid>())).Returns(Task.FromResult(interviewReqList));
            
            var result = await sut.GetInterviewByJobId(interviewGuid);
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ContentResult));
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task GetInterviewsByLastName_ReturnsOk_WhenModelIsValid()
        {
            mockInterviewDao
                .Setup(x => x.GetInterviewByLastName(It.IsAny<string>())).Returns(Task.FromResult(interviewJoinCanList));

            var result = await sut.GetInterviewByLastName("Miller");
            var okResult = result as ContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ContentResult));
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

        }

    }
}
