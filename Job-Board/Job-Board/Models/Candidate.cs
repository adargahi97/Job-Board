using System;

namespace Job_Board.Models
{
    public class Candidate
    {
        public object NewCandidate { get; set; }

        public void AddCandidate(Candidate expectedCandidate)
        {
            NewCandidate = expectedCandidate;
        }
    }
}
