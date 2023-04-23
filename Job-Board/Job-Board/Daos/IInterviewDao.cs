using Job_Board.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface IInterviewDao
    {
        Task CreateInterview(InterviewRequest interview);
        Task<IEnumerable<InterviewDailySearch>> GetInterviewsByDate(DateTime dt);
        Task DeleteInterviewById(Guid id);
        Task<InterviewRequest> GetInterviewByID(Guid id);
        Task<Interview> UpdateInterviewById(Interview interview);
        Task<IEnumerable<JobPosting>> CheckJobPostingExists(string position);
        Task<IEnumerable<JobPostingDailySearchByPosition>> DailySearchByPosition(string position);

    }
}
