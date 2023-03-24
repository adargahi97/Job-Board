using Job_Board.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface IJobPostingDao
    {
        Task CreateJobPosting(JobPostingRequest jobPosting);
        Task<IEnumerable<JobPosting>> GetJobPostings();
        Task DeleteJobPostingById(Guid id);
        Task<JobPostingRequest> GetJobPostingByID(Guid id);
        Task<JobPosting> GetJobPostingByPosition(string position);
        Task<JobPostingRequest> GetJobPostingByLocationId(Guid locationId);
        Task<JobPosting> UpdateJobPostingById(JobPosting jobPosting);
    }
}
