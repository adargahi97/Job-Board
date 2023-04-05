using Dapper;
using Job_Board.Models;
using Job_Board.Wrappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Job_Board.Daos
{
    public class SearchDao : ISearchDao
    {

        private readonly ISqlWrapper sqlWrapper;

        public SearchDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        public async Task<IEnumerable<LocationByState>> GetLocationByState(string state)
        {
            var query = $"SELECT * FROM Location WHERE State = '{state}'";

            using (sqlWrapper.CreateConnection())
            {
                var location = await sqlWrapper.QueryAsync<LocationByState>(query);
                return location.ToList();
            }
        }

        public async Task<IEnumerable<JobPostingDailySearchByPosition>> DailySearchByPosition(string position)
        {
            var query = $" SELECT Position, Department, Candidate.FirstName, Candidate.LastName, Interview.Date, Location.Building " +
                            $"FROM JobPosting " +
                            $"INNER JOIN Candidate ON JobPosting.Id = Candidate.JobId " +
                            $"INNER JOIN Interview ON JobPosting.Id = Interview.JobId " +
                            $"INNER JOIN Location ON JobPosting.LocationId = Location.Id WHERE Position = '{position}'";

            using (sqlWrapper.CreateConnection())
            {
                var candidates = await sqlWrapper.QueryAsync<JobPostingDailySearchByPosition>(query);
                return candidates.ToList();
            }
        }

    }
}
