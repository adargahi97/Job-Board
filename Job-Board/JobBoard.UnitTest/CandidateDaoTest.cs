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
        void CallSqlWithString()
        {
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            mockSqlWrapper.Setup(o => o.Query<Candidate>(It.IsAny<string>())).Returns(new List<Candidate>());
            ISqlWrapper customMockSqlWrapper = new MockSqlWrapper();
            CandidateDao sut = new CandidateDao(customMockSqlWrapper);

            sut.GetCandidate();

            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query<List<Candidate>>(It.Is<string>(sql=>sql=="SELECT * FROM [DBO].[JOBBOARD]")),Times.Once);

        }
    }
}
