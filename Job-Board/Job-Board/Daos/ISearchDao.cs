using Job_Board.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface ISearchDao
    {

        Task<IEnumerable<LocationByState>> GetLocationByState(string state);
        Task<IEnumerable<CandidateByLastName>> GetCandidateByLastName(string lastName);
        Task<IEnumerable<CandidateByJobId>> GetCandidateByJobId(Guid jobId);
        Task<IEnumerable<Interview>> GetInterviewByJobId(Guid jobId);
        Task<IEnumerable<InterviewJoinCandidate>> GetInterviewByLastName(string lastName);
    }
}
