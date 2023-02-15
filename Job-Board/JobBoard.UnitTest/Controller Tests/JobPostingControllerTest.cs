using Job_Board.Controllers;
using Job_Board.Daos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.UnitTest.Controller_Tests
{
    [TestClass]
    public class JobPostingControllerTest
    {

        [TestMethod]

        public void CallDao()
        {
            Mock<IJobPostingDao> mockJobPostingDao = new Mock<IJobPostingDao>();

            JobPostingController sut = new JobPostingController(mockJobPostingDao.Object);

            //sut.CallDao();

            mockJobPostingDao.Verify(jobPostingDao => jobPostingDao.GetJobPosting(), Times.Once);
        }
    }
}
