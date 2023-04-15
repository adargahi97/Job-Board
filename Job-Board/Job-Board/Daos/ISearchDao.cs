using Job_Board.Models;
using Job_Board.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Job_Board.Daos
{
    public interface ISearchDao
    {
        Task<IEnumerable<LocationByState>> GetLocationByState(string state);
        Task<IEnumerable<JobPostingByState>> GetJobPostingByState(string state);
        Task<IEnumerable<CandidateByLastName>> GetCandidateByLastName(string lastName);
        Task<IEnumerable<CandidateByJobId>> GetCandidateByJobId(Guid jobId);
        Task<IEnumerable<Interview>> GetInterviewByJobId(Guid jobId);
        Task<IEnumerable<InterviewJoinCandidate>> GetInterviewByLastName(string lastName);
        Task<JobPostingByPosition> GetJobPostingByPosition(string position);
        Task<IEnumerable<JobPostingByLocationId>> GetJobPostingByLocationId(Guid locationId);
        Task<LocationByBuilding> GetLocationByBuilding(string building);
    }
}