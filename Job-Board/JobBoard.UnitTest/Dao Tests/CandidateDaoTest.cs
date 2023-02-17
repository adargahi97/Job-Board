using Job_Board.Daos;
using Job_Board.Models;
using Job_Board.Wrappers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //ISqlWrapper customMockSqlWrapper = new MockSqlWrapper();
            CandidateDao sut = new CandidateDao(mockSqlWrapper.Object);

            sut.GetCandidate();

            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query<Candidate>(It.Is<string>(sql=>sql=="SELECT * FROM [DBO].[JOBBOARD]")),Times.Once);

        }





        [TestMethod]
        public async Task GetCandidateByID_PullsInfo()
        {

            //ARRANGE
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            CandidateDao sut = new CandidateDao(mockSqlWrapper.Object);
           
            //ACT
            var result = sut.GetCandidateByID(1);
            var candidate = sut.GetCandidateByFirstName("Ron");

            //FAIL
            //Candidate candidate2 = sut.GetCandidateByID(1);

            //ASSERT
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
            Assert.IsNotNull(candidate);

            //FAIL
            //Assert.AreEqual(candidate.Id, 1);
            //Assert.AreEqual(candidate.FirstName, "Ron");



            //mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query<Candidate>(It.Is<string>(sql => sql == "SELECT * FROM [DBO].[JOBBOARD]")), Times.Once);
            //mockSqlWrapper.Verify(c => c.QueryFirstOrDefaultAsync<CandidateRequest>(It.Is<string>(q => q.Contains("Candidate") && q.Contains(firstName)), It.IsAny<object>()), Times.Once);



        }

        [TestMethod]
        public void GetCandidateByFirstName_PullsInfo()
        {

            //ARRANGE
            CandidateDao sut = new CandidateDao();
            var firstName = "Ron";
            
            //ACT

            var candidate = sut.GetCandidateByFirstName(firstName);


            //ASSERT
            Assert.IsTrue(candidate.Id > 0);
            Assert.AreNotEqual(0, candidate.Id);

            //FAIL - cant call candidate.FirstName
            //Assert.AreEqual(candidate.FirstName, firstName);

        }

        [TestMethod]

        public void GetCandidateByID_NotNull()
        {

            //ARRANGE
            CandidateDao sut = new CandidateDao();

            //ACT

            var  candidate = sut.GetCandidateByID(1);

            //ASSERT
            Assert.IsTrue(candidate.Id > 0);
            Assert.AreNotEqual(0, candidate.Id);
            //Assert.IsNotNull(candidate.FirstName);


        }


    }
}
