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

    }
}
