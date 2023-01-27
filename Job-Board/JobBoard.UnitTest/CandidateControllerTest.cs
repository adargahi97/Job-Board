using Job_Board.Controllers;
using Job_Board.Daos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.UnitTest
{
    [TestClass]

    public class CandidateControllerTest
    {
        [TestMethod]
        public void CallDao()
        {
            Mock<ICandidateDao> mockCandidateDao = new Mock<ICandidateDao>();

            CandidateController sut = new CandidateController(mockCandidateDao.Object);

            sut.CallDao();

            mockCandidateDao.Verify(CandidateDao => CandidateDao.GetCandidate(), Times.Once);
        }
    }
}
