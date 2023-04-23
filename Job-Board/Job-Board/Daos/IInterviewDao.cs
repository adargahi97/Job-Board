using Job_Board.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface IInterviewDao
    {
        Task CreateInterview(InterviewRequest interview);
        Task<IEnumerable<InterviewJoin>> GetInterviewsByDate(DateTime dt);
        Task DeleteInterviewById(Guid id);
        Task<InterviewRequest> GetInterviewById(Guid id);
        Task<Interview> UpdateInterviewById(Interview interview);
        Task<IEnumerable<JobPosting>> CheckJobPostingExists(string position);
        Task<IEnumerable<InterviewJoin>> GetInterviewsByPosition(string position);
        Task<IEnumerable<InterviewRequest>> GetInterviewByJobId(Guid jobId);
        Task<IEnumerable<InterviewJoinCandidate>> GetInterviewByLastName(string lastName);
    }
}
