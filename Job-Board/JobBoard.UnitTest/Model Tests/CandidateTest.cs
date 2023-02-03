using Job_Board.Models;

namespace JobBoard.UnitTest
{
    [TestClass]
    public class CandidateTest
    {
        [TestMethod]
        public void AddCandidate()
        {
            Candidate sut = new Candidate();
            Candidate expectedCandidate = new Candidate();

            

            sut.AddCandidate(expectedCandidate);

            Assert.AreEqual(expectedCandidate, sut.NewCandidate);
        }
    }
}