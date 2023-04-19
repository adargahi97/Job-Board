using System;

namespace Job_Board.Models
{
    public class Interview
    {
        /// <summary>
        /// Interview Id
        /// </summary>
        /// <example>D4138EFE-C728-408B-88E5-0D12E681DE19</example>
        public Guid Id { get; set; }
        /// <summary>
        /// Interview's Date and Time
        /// </summary>
        /// <example>2023-01-01 12:00:00</example>
        public string DateTime { get; set; }
        /// <summary>
        /// Interview's Location Id
        /// </summary>
        /// <example>FC27A8EF-1755-4A98-BE38-B9F5BC202CD4</example>
        public Guid LocationId { get; set; }
        /// <summary>
        /// Interviewee's Id
        /// </summary>
        /// <example>D7748F96-FB3A-4DFA-8B28-7D0B4AAB3DB7</example>
        public Guid CandidateId { get; set; }
        /// <summary>
        /// Job Id
        /// </summary>
        /// <example>0AB667E2-3936-48EC-B1DA-E7614239B788</example>
        public Guid JobId { get; set; }

    }
    public class InterviewRequest
    {
        /// <summary>
        /// Interview's Date and Time
        /// </summary>
        /// <example>2023-01-01 12:00:00</example>
        public string DateTime { get; set; }
        /// <summary>
        /// Interview's Location Id
        /// </summary>
        /// <example>FC27A8EF-1755-4A98-BE38-B9F5BC202CD4</example>
        public Guid LocationId { get; set; }
        /// <summary>
        /// Interviewee's Id
        /// </summary>
        /// <example>D7748F96-FB3A-4DFA-8B28-7D0B4AAB3DB7</example>
        public Guid CandidateId { get; set; }
        /// <summary>
        /// Job Id
        /// </summary>
        /// <example>0AB667E2-3936-48EC-B1DA-E7614239B788</example>
        public Guid JobId { get; set; }
    }

    public class InterviewJoinCandidate
    {
        /// <summary>
        /// Candidate's First Name
        /// </summary>
        /// <example>Jonathan</example>
        public string FirstName { get; set; }
        /// <summary>
        /// Candidate's Last Name
        /// </summary>
        /// <example>Miller</example>
        public string LastName { get; set; }
        /// <summary>
        /// Interview's Date and Time
        /// </summary>
        /// <example>2023-01-01 12:00:00</example>
        public string DateTime { get; set; }
        /// <summary>
        /// Interview's Location Id
        /// </summary>
        /// <example>FC27A8EF-1755-4A98-BE38-B9F5BC202CD4</example>
        public Guid LocationId { get; set; }
        /// <summary>
        /// Job Id
        /// </summary>
        /// <example>0AB667E2-3936-48EC-B1DA-E7614239B788</example>
        public Guid JobId { get; set; }

    }
    public class InterviewDailySearch
    {
        /// <summary>
        /// Candidate's First Name
        /// </summary>
        /// <example>Jonathan</example>
        public string FirstName { get; set; }
        /// <summary>
        /// Candidate's Last Name
        /// </summary>
        /// <example>Miller</example>
        public string LastName { get; set; }
        /// <summary>
        /// Interview's Date and Time
        /// </summary>
        /// <example>2023-01-01 12:00:00</example>
        public string DateTime { get; set; }
        /// <summary>
        /// Job Position
        /// </summary>
        /// <example>Intern</example>
        public string Position { get; set; }
        /// <summary>
        /// Building Location
        /// </summary>
        /// <example>Tango</example>
        public string Building { get; set; }

    }

}
