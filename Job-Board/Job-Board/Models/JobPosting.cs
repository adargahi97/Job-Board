using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Job_Board.Models
{
    public class JobPosting
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public int LocationsId { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public JobPosting NewJobPosting { get; private set; }

        public void AddJobPosting(JobPosting expectedJobPosting)
        {
            NewJobPosting = expectedJobPosting;
        }

    }
    public class JobPostingRequest
    {
        public string Position { get; set; }
        public int LocationsId { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
    }
}
