using Dapper;
using Job_Board;
using Job_Board.Daos;
using Job_Board.Models;
using Job_Board.Wrappers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Moq;
using Moq.Language.Flow;
using System.Data;

namespace JobBoard.UnitTest
{
    [TestClass]
    public class CandidateDaoTest
    {

        Mock<ISqlWrapper> mockSqlWrapper;
        CandidateDao sut;
        Guid candidateGuid;
        List<Candidate> candidate;
        CandidateRequest candidateRequest;

        [TestInitialize]
        public void Initialize()
        {
            mockSqlWrapper = new Mock<ISqlWrapper>();
            sut = new CandidateDao(mockSqlWrapper.Object);
            candidateGuid = new Guid("125f1ad4-41b9-4e07-9412-51bb1b34f736");
            candidate = new List<Candidate>()
            {
                new Candidate()
                {
                    Id =  new Guid("125f1ad4-41b9-4e07-9412-51bb1b34f736"),
                    FirstName = "Abbas",
                    LastName = "Dargahi",
                    PhoneNumber = "555-555-0008",
                    JobId = new Guid("e4203d53-03e3-4121-bead-94a14248f1ea")
                }
            };
            candidateRequest = new CandidateRequest()
            {
                FirstName = "Abbas",
                LastName = "Dargahi",
                PhoneNumber = "555-555-0008",
                JobId = new Guid("e4203d53-03e3-4121-bead-94a14248f1ea")
            };

        }

        [TestCleanup]
        public void Cleanup()
        {
            mockSqlWrapper = null;
            sut = null;
            candidateGuid = new Guid();
            candidate = null;
            candidateRequest = null;
        }

        [TestMethod]
        public void CreateCandidate_ShouldCreateEntry()
        {
            _ = sut.CreateCandidate(candidateRequest);

            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "INSERT INTO Candidate (FirstName, LastName, PhoneNumber, JobId, InterviewId)" +
                "VALUES (@FirstName, @LastName, @PhoneNumber, @JobId, @InterviewId)"), It.IsAny<DynamicParameters>()), Times.Once);

        }

        [TestMethod]
        public void GetCandidateById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.GetCandidateById(candidateGuid);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryFirstOrDefaultAsync<LocationRequest>(It.Is<string>(sql => sql == $"SELECT * FROM Candidate WHERE Id = '{candidateGuid}'")), Times.Once);
        }

    }

}
