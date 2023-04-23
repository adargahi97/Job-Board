using Job_Board.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface IJobPostingDao
    {
        Task CreateJobPosting(JobPostingRequest jobPosting);
        Task DeleteJobPostingById(Guid id);
        Task<JobPostingRequest> GetJobPostingById(Guid id);
        Task<JobPosting> UpdateJobPostingById(JobPosting jobPosting);
        Task<IEnumerable<JobPostingJoin>> GetJobPostingByBuilding(string building);
        Task<JobPostingJoin> GetJobPostingByPosition(string position);

    }
}
