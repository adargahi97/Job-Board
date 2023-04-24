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
    public class InterviewDaoTest
    {

        Mock<ISqlWrapper> mockSqlWrapper;
        InterviewDao sut;
        Guid interviewGuid;
        List<Interview> interview;
        InterviewRequest interviewRequest;

        [TestInitialize]
        public void Initialize()
        {
            mockSqlWrapper = new Mock<ISqlWrapper>();
            sut = new InterviewDao(mockSqlWrapper.Object);
            interviewGuid = new Guid("d4138efe-c728-408b-88e5-0d12e681de19");
            interview = new List<Interview>()
            {
                new Interview()
                {
                    Id = new Guid("d4138efe-c728-408b-88e5-0d12e681de19"),
                    DateTime = "2023-04-21",
                    LocationId = new Guid("fc27a8ef-1755-4a98-be38-b9f5bc202cd4"),
                    CandidateId = new Guid("d7748f96-fb3a-4dfa-8b28-7d0b4aab3db7"),
                    JobId = new Guid("0ab667e2-3936-48ec-b1da-e7614239b788")

                }
            };
            interviewRequest = new InterviewRequest()
            {
                DateTime = "2023-04-21",
                LocationId = new Guid("fc27a8ef-1755-4a98-be38-b9f5bc202cd4"),
                CandidateId = new Guid("d7748f96-fb3a-4dfa-8b28-7d0b4aab3db7"),
                JobId = new Guid("0ab667e2-3936-48ec-b1da-e7614239b788")
            };

        }

        [TestCleanup]
        public void Cleanup()
        {
            mockSqlWrapper = null;
            sut = null;
            interviewGuid = new Guid();
            interview = null;
            interviewRequest = null;
        }

        [TestMethod]
        public void CreateInterview_ShouldCreateEntry()
        {
            _ = sut.CreateInterview(interviewRequest);

            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "INSERT INTO Interview (DateTime, LocationId, CandidateId, JobId) " +
                "VALUES (@DateTime, @LocationId, @CandidateId, @JobId)"), It.IsAny<DynamicParameters>()), Times.Once);

        }

        [TestMethod]
        public void GetInterviewById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.GetInterviewById(interviewGuid);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryFirstOrDefaultAsync<LocationRequest>(It.Is<string>(sql => sql == $"SELECT Id, CONVERT(VARCHAR(20),DateTime,0) AS DateTime, JobId, LocationId, CandidateId FROM Interview WHERE Id = '{interviewGuid}'")), Times.Once);
        }

    }



}
