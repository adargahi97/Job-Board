using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;

namespace Job_Board.Models
{
    public class JobPosting
    {
        public Guid Id { get; set; }
        public string Position { get; set; }
        public Guid LocationId { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }

    }
    public class JobPostingRequest
    {
        public string Position { get; set; }
        public Guid LocationId { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
    }

    public class JobPostingByPosition
    { 
        public string Department { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
    }
    public class JobPostingDailySearchByPosition
    {
        public string Position { get; set; }
        public string Department { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateTime { get; set; }
        public string Building { get; set; }

    }
    public class JobPostingByLocationId
    {
        public string Position { get; set; }
        public string Department { get; set; }

    }
}
