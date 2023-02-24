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
        [TestMethod]
        public void CallSqlWithString()
        {
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            //mockSqlWrapper.Setup(o => o.Query<Candidate>(It.IsAny<string>())).Returns(new List<Candidate>());
            ISqlWrapper customMockSqlWrapper = new MockSqlWrapper();
            CandidateDao sut = new CandidateDao(mockSqlWrapper.Object);

            sut.GetCandidate();

            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query<Candidate>(It.Is<string>(sql => sql == "SELECT * FROM [DBO].[JOBBOARD]")), Times.Once);

        }



        [TestMethod]
        public void GetCandidateByID_PullsInfo()
        {

            //ARRANGE
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            CandidateDao sut = new CandidateDao(mockSqlWrapper.Object);

            Candidate candidate = new Candidate()
            {
                FirstName = "Don",
                LastName = "Jon",
                PhoneNumber = "555-555-5555",
                Job_Id = 8,
                LocationsId = 8,
                Id = 8,
            };

            //ACT
            var result = sut.GetCandidateByID(candidate.Id).Result;
            var result2 = sut.GetCandidateByID(1).Result;


            //mockSqlWrapper.Setup(w => w.Query<CandidateRequest>(It.IsAny<string>())).Returns(new List<Candidate>());


            //ASSERT
            Assert.IsNotNull(result);
            Assert.IsTrue(result.LocationsId > 0);
            Assert.IsNotNull(candidate);
            Assert.AreEqual(candidate.Job_Id, 8);
            Assert.AreEqual(result.LocationsId, 8);
            Assert.AreEqual(result2.LocationsId, 1);


            //mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query<Candidate>(It.Is<string>(sql => sql == $"SELECT * FROM Candidate WHERE Id = {id}")), Times.Once);
            //mockSqlWrapper.Verify(c => c.QueryFirstOrDefaultAsync<CandidateRequest>(It.Is<string>(q => q.Contains("Candidate") && q.Contains(firstName)), It.IsAny<object>()), Times.Once);



        }

        [TestMethod]
        public void GetCandidateByFirstName_PullsInfo()
        { 

            //ARRANGE
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            CandidateDao sut = new CandidateDao(mockSqlWrapper.Object);

            //ACT
            var candidate = sut.GetCandidateByFirstName("Ron").Result;

            //Assert
            Assert.AreEqual(candidate.FirstName, "Ron");
        }

       

        [TestMethod]

        public void GetCandidateByID_NotNull()
        {

            //ARRANGE
            CandidateDao sut = new CandidateDao();

            //ACT

            var candidate = sut.GetCandidateByID(1);

            //ASSERT
            Assert.IsTrue(candidate.Id > 0);
            Assert.IsNotNull(candidate.Id);

        }


        [TestMethod]
        public async Task DeleteCandidateByID_Works()
        {

            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();

            CandidateDao sut = new CandidateDao();
            int id = 8;

            Candidate candidate = new Candidate()
            {
                FirstName = "Don",
                LastName = "Jon",
                PhoneNumber = "555-555-5555",
                Job_Id = 8,
                LocationsId = 8,
                Id = 8,
            };

            // Act
            await sut.DeleteCandidateById(8);

            // Assert

            //Assert.IsNull(candidate.Id);
            //Assert.IsNotNull(candidate.Id);
            //Assert.IsTrue(candidate.Id > 0);



            //mockSqlWrapper.Verify(c => c.ExecuteAsync($"DELETE FROM Candidate WHERE Id = {id}"), Times.Once);
        }


    }
}
