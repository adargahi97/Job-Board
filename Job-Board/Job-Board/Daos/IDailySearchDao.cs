using Job_Board.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface IDailySearchDao
    {

        Task<IEnumerable<Interview>> GetInterviewsByDate(DateTime dt);
        Task<IEnumerable<Interview>> GetTodaysInterviews();

        Task<IEnumerable<JobPosting>> CheckJobPostingExists(string position);

        Task<IEnumerable<JobPostingDailySearchByPosition>> DailySearchByPosition(string position);

    }
}