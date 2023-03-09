using Job_Board.Models;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface IJobPostingDao
    {
        //public void GetJobPosting();


        Task DeleteJobPostingById(int id);

        Task<JobPostingRequest> GetJobPostingByID(int id);


        Task<JobPosting> GetJobPostingByPosition(string position);

        Task<JobPostingRequest> GetJobPostingByLocationsId(int locationsId);


        Task<JobPosting> UpdateJobPostingById(JobPosting jobPosting);
    }
}
