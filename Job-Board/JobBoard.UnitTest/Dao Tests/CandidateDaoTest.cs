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
        //[TestMethod]
        //public void CallSqlWithString()
        //{
        //    Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
        //    //mockSqlWrapper.Setup(o => o.Query<Candidate>(It.IsAny<string>())).Returns(new List<Candidate>());
        //    ISqlWrapper customMockSqlWrapper = new MockSqlWrapper();
        //    CandidateDao sut = new CandidateDao(mockSqlWrapper.Object);

        //    sut.GetCandidate();

        //    mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query<Candidate>(It.Is<string>(sql => sql == "SELECT * FROM [DBO].[JOBBOARD]")), Times.Once);

        //}



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
            string name = "Ron";

            //ACT
            _ = sut.GetCandidateByFirstName(name);
            //Assert
            //Assert.AreEqual(candidate.FirstName, "Ron");
            mockSqlWrapper.Verify(x => x.QueryFirstOrDefaultAsync<Candidate>(It.Is<string>(sql => sql == $"GET FROM Candidate WHERE FirstName = {name}")), Times.Once);

        }



        [TestMethod]

        public void GetCandidateByID_NotNull()
        {

            //ARRANGE
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();

            CandidateDao sut = new CandidateDao(mockSqlWrapper.Object);

            //ACT

            var candidate = sut.GetCandidateByID(1);

            //ASSERT
            Assert.IsTrue(candidate.Id > 0);
            Assert.IsNotNull(candidate.Id);

        }


        [TestMethod]
        public void DeleteCandidateByID_Works()
        {

            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();

            CandidateDao sut = new CandidateDao(mockSqlWrapper.Object);
            int id = 1;

            // Act
            _ = sut.DeleteCandidateById(id);

            // Assert

            mockSqlWrapper.Verify(x => x.ExecuteAsync(It.Is<string>(sql => sql == $"DELETE FROM Candidate WHERE Id = {id}")), Times.Once); ;
        }

        [TestMethod]
        public void DeleteCandidateById_Works()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            CandidateDao sut = new CandidateDao(mockSqlWrapper.Object);

            // Act
            _ = sut.DeleteCandidateById(1);

            // Assert
            mockSqlWrapper.Verify(x => x.ExecuteAsync(It.Is<string>(sql => sql == "DELETE FROM Candidate WHERE Id = 1")), Times.Once); ;

        }

    }
}
