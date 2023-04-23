using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;

namespace Job_Board.Models
{
    public class JobPosting
    {
        /// <summary>
        /// Job Posting's Id
        /// </summary>
        /// <example>0AB667E2-3936-48EC-B1DA-E7614239B788</example>
        public Guid Id { get; set; }
        /// <summary>
        /// Job Position
        /// </summary>
        /// <example>Intern</example>
        public string Position { get; set; }
        /// <summary>
        /// Interview's Location Id
        /// </summary>
        /// <example>FC27A8EF-1755-4A98-BE38-B9F5BC202CD4</example>
        public Guid LocationId { get; set; }
        /// <summary>
        /// Job Listing's Department
        /// </summary>
        ///<example>Marketing</example>
        public string Department { get; set; }
        /// <summary>
        /// Job Listing's Description
        /// </summary>
        ///<example>Marketing Intern</example>
        public string Description { get; set; }

    }
    public class JobPostingRequest
    {
        /// <summary>
        /// Job Position
        /// </summary>
        /// <example>Intern</example>
        public string Position { get; set; }
        /// <summary>
        /// Interview's Location Id
        /// </summary>
        /// <example>FC27A8EF-1755-4A98-BE38-B9F5BC202CD4</example>
        public Guid LocationId { get; set; }
        /// <summary>
        /// Job Listing's Department
        /// </summary>
        ///<example>Marketing</example>
        public string Department { get; set; }
        /// <summary>
        /// Job Listing's Description
        /// </summary>
        ///<example>Marketing Intern</example>
        public string Description { get; set; }
    }

    public class JobPostingJoin
    {
        /// <summary>
        /// Job Posting's Id
        /// </summary>
        /// <example>0AB667E2-3936-48EC-B1DA-E7614239B788</example>
        public Guid Id { get; set; }
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
        /// <summary>
        /// Job Listing's Description
        /// </summary>
        ///<example>Marketing Intern</example>
        public string Description { get; set; }
        /// <summary>
        /// Location's City
        /// </summary>
        ///<example>Portland</example>

        public string City { get; set; }
        /// <summary>
        /// Location's State
        /// </summary>
        /// <example>OR</example>
        public string State { get; set; }
        /// <summary>
        /// Building Location
        /// </summary>
        /// <example>Tango</example>
        public string Building { get; set; }

    }
}
