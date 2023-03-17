using System;

namespace Job_Board.Models
{
    public class Candidate
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid JobId { get; set; }
        public Guid LocationsId { get; set; }

    }
    public class CandidateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid JobId { get; set; }
        public Guid LocationsId { get; set; }
    }

    public class CandidateId
    {
        public Guid Id { get; set; }
    }

}
