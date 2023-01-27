using System;

namespace Job_Board.Models
{
    public class Candidate
    {
        public object NewCandidate { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Job_Id { get; set; }
        public int LocationsId { get; set; }

        public void AddCandidate(Candidate expectedCandidate)
        {
            NewCandidate = expectedCandidate;
            //throw new NotImplementedException();
        }
    }
}
