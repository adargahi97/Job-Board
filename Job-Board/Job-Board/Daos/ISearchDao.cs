using Job_Board.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface ISearchDao
    {

        Task<IEnumerable<LocationByState>> GetLocationByState(string state);
        Task<IEnumerable<JobPostingDailySearchByPosition>> DailySearchByPosition(string position);
        Task<IEnumerable<CandidateByLastName>> GetCandidateByLastName(string lastName);
        Task<IEnumerable<CandidateByJobId>> GetCandidateByJobId(Guid jobId);
        Task<IEnumerable<Interview>> GetInterviewByJobId(Guid jobId);
        Task<IEnumerable<InterviewJoinCandidate>> GetInterviewByLastName(string lastName);
        Task<IEnumerable<Interview>> GetInterviewsByDate(DateTime dt);
        Task<IEnumerable<Interview>> GetTodaysInterviews();

        Task<IEnumerable<JobPosting>> CheckJobPostingExists(string position);
    }
}
