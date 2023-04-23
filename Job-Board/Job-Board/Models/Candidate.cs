using Job_Board.Models;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel;

namespace Job_Board.Models
{
    public class Candidate
    {
        /// <summary>
        /// Candidate Id
        /// </summary>
        /// <example>221D5EC3-D99A-4A47-8E74-2411328E99D2</example>
        public Guid Id { get; set; }
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
        /// Candidate's Job Id
        /// </summary>
        /// <example>85DA0EA5-101C-45BC-9388-9C64898363FE</example>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Candidate's Interview Id
        /// </summary>
        /// <example>F140AC1A-4941-46D5-8A4E-C1CA19E24C3F</example>
        public Guid JobId { get; set; }

    }
    public class CandidateRequest
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
        /// Candidate's Phone Number
        /// </summary>
        /// <example>314-992-3234</example>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Candidate's Job Id
        /// </summary>
        /// <example>85DA0EA5-101C-45BC-9388-9C64898363FE</example>
        public Guid JobId { get; set; }
    }

    public class CandidateByLastName
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
        /// Candidate's Phone Number
        /// </summary>
        /// <example>314-992-3234</example>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Job Position
        /// </summary>
        /// <example>Intern</example>
        public string Position { get; set; }
        /// <summary>
        /// Job Listing's Department
        /// </summary>
        ///<example>Marketing</example>
        public string Department { get; set; }
    }

    public class CandidateByJobId
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
        /// Candidate's Phone Number
        /// </summary>
        /// <example>314-992-3234</example>
        public string PhoneNumber { get; set; }
    }
}