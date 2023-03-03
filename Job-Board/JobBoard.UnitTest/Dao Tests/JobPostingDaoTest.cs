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
    public class JobPostingDaoTest
    {
        //[TestMethod]
        //void CallSqlWithString()
        //{

        //    Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();

        //    JobPostingDao sut = new JobPostingDao(mockSqlWrapper.Object);

        //    sut.GetJobPosting();

        //    mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query<JobPosting>(It.Is<string>(sql => sql == "SELECT * FROM [DBO].[JOBBOARD]")), Times.Once);

        //}
    }
}
