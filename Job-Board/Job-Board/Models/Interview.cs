using System;

namespace Job_Board.Models
{
    public class Interview
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public Guid LocationId { get; set; }
        public Guid CandidateId { get; set; }
        public Guid JobId { get; set; }

    }
    public class InterviewRequest
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public Guid LocationId { get; set; }
        public Guid CandidateId { get; set; }
        public Guid JobId { get; set; }
    }
}
