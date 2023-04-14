using System;

namespace Job_Board.Models
{
    public class Interview
    {
        public Guid Id { get; set; }
        public string DateTime { get; set; }
        public Guid LocationId { get; set; }
        public Guid CandidateId { get; set; }
        public Guid JobId { get; set; }

    }
    public class InterviewRequest
    {
        public string DateTime { get; set; }
        public Guid LocationId { get; set; }
        public Guid CandidateId { get; set; }
        public Guid JobId { get; set; }
    }

    public class InterviewJoinCandidate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateTime { get; set; }
        public Guid LocationId { get; set; }
        public Guid JobId { get; set; }

    }
    public class InterviewByDate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateTime { get; set; }
        public Guid LocationId { get; set; }
        public Guid JobId { get; set; }

    }
    public class InterviewDailySearch
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateTime { get; set; }
        public string Position { get; set; }
        public string Building { get; set; }

    }

}
