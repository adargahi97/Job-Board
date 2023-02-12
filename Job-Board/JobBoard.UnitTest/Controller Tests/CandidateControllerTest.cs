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
    public class CandidateControllerTests
    {
        private CandidateController _candidateController;
        private Mock<ICandidateDao> mockCandidateDao;

        [Setup]
        public void Setup()
        {
            mockCandidateDao = new Mock<ICandidateDao>();

        }

        [TestMethod]
        public void GetCandidates_ReturnsAllCandidates()
        {
            CandidateController sut = new CandidateController(mockCandidateDao);

            sut.GetCandidates();

            mockCandidateDao.Verify(CandidateDao => CandidateDao.GetCandidate(), Times.Once);
        }
    }
}
