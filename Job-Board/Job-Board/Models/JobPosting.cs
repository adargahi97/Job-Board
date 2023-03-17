using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;

namespace Job_Board.Models
{
    public class JobPosting
    {
        public Guid Id { get; set; }
        public string Position { get; set; }
        public Guid LocationsId { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }

    }
    public class JobPostingRequest
    {
        public string Position { get; set; }
        public Guid LocationsId { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
    }
}
