using Job_Board.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface IJobPostingDao
    {
        Task CreateJobPosting(JobPostingRequest jobPosting);
        Task<IEnumerable<JobPosting>> GetJobPostings();
        Task DeleteJobPostingById(int id);
        Task<JobPostingRequest> GetJobPostingByID(int id);
        Task<JobPosting> GetJobPostingByPosition(string position);
        Task<JobPostingRequest> GetJobPostingByLocationsId(int locationsId);
        Task<JobPosting> UpdateJobPostingById(JobPosting jobPosting);
    }
}
