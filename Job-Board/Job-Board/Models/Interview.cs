using System;

namespace Job_Board.Models
{
    public class Interview
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int LocationsId { get; set; }
        public int CandidateId { get; set; }
        public int Job_Id { get; set; }

        public Interview expectedInterview { get; set; }
        public Interview NewInterview { get; private set; }

        public void AddInterview(Interview expectedInterview)
        {
            NewInterview = expectedInterview;
        }
    }
    public class InterviewRequest
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public int LocationsId { get; set; }
        public int CandidateId { get; set; }
        public int Job_Id { get; set; }
    }
}
