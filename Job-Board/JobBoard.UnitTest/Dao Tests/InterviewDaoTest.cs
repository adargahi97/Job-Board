using Job_Board.Daos;
using Job_Board.Models;
using Job_Board.Wrappers;
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
        //[TestMethod]
        //void CallSqlWithString()
        //{
        //    Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();

        //    InterviewDao sut = new InterviewDao(mockSqlWrapper.Object);

        //    sut.GetInterview();

        //    mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query<Interview>(It.Is<string>(sql => sql == "SELECT * FROM [DBO].[JOBBOARD]")), Times.Once);
        //}



        [TestMethod]
        public void DeleteInterviewByID_Works()
        {

            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();

            InterviewDao sut = new InterviewDao(mockSqlWrapper.Object);
            int id = 1;

            // Act
            _ = sut.DeleteInterviewById(id);

            // Assert

            mockSqlWrapper.Verify(x => x.ExecuteAsync(It.Is<string>(sql => sql == $"DELETE FROM Interview WHERE Id = {id}")), Times.Once); ;
        }

        [TestMethod]
        public void GetInterviewByID_NotNull()
        {

            //ARRANGE
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();

            InterviewDao sut = new InterviewDao(mockSqlWrapper.Object);

            //ACT

            var candidate = sut.GetInterviewByID(1);

            //ASSERT
            Assert.IsTrue(candidate.Id > 0);
            Assert.IsNotNull(candidate.Id);

        }

        [TestMethod]
        public void GetInterviewByCandidateId_PullsInfo()
        {

            //ARRANGE
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            InterviewDao sut = new InterviewDao(mockSqlWrapper.Object);
            int candidateId = 1;

            //ACT
            _ = sut.GetInterviewByCandidateId(candidateId);

            //Assert
            mockSqlWrapper.Verify(x => x.QueryFirstOrDefaultAsync<InterviewRequest>(It.Is<string>(sql => sql == $"GET FROM Interview WHERE CandidateId = {candidateId}")), Times.Once);

        }

        [TestMethod]
        public void GetInterviewByJob_Id_PullsInfo()
        {

            //ARRANGE
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            InterviewDao sut = new InterviewDao(mockSqlWrapper.Object);
            int job_Id = 1;

            //ACT
            _ = sut.GetInterviewByJob_Id(job_Id);

            //Assert
            mockSqlWrapper.Verify(x => x.QueryFirstOrDefaultAsync<InterviewRequest>(It.Is<string>(sql => sql == $"GET FROM Interview WHERE Job_Id = {job_Id}")), Times.Once);

        }

    }
}
