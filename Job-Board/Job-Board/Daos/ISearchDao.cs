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

    }
}
