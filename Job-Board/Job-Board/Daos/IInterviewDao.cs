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
        Task<Interview> UpdateInterviewById(Interview interview);

    }
}
