using Job_Board.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface IInterviewDao
    {
        Task CreateInterview(InterviewRequest interview);
        Task<IEnumerable<Interview>> GetInterviews();
        Task DeleteInterviewById(Guid id);
        Task<InterviewRequest> GetInterviewByID(Guid id);
        Task<InterviewRequest> GetInterviewByCandidateId(Guid candidateId);
        Task<Interview> UpdateInterviewById(Interview interview);
        Task<IEnumerable<Interview>> GetInterviewByJobId(Guid jobId);

        Task<InterviewJoinCandidate> GetInterviewByLastName(string lastName);

    }
}
