using Dapper;
using Job_Board.Models;
using Job_Board.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Job_Board.Daos
{
    public class DailySearchDao : IDailySearchDao
    {

        private readonly ISqlWrapper sqlWrapper;

        public DailySearchDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        public async Task<IEnumerable<Interview>> GetInterviewsByDate(DateTime dt)
        {

            var query = $"SELECT * FROM Interview WHERE '{dt}' = CAST(DateTime AS DATE)";

            using (var connection = sqlWrapper.CreateConnection())
            {
                var interviews = await connection.QueryAsync<Interview>(query);

                return interviews.ToList();
            }
        }
        public async Task<IEnumerable<Interview>> GetTodaysInterviews()
        {

            var query = $"SELECT * FROM Interview WHERE CAST(DateTime AS DATE) = CAST(GETDATE() AS DATE)";

            using (var connection = sqlWrapper.CreateConnection())
            {
                var interviews = await connection.QueryAsync<Interview>(query);

                return interviews.ToList();
            }
        }

        public async Task<IEnumerable<JobPosting>> CheckJobPostingExists(string position)
        {

            var query = "SELECT Position FROM JobPosting";



            using (var connection = sqlWrapper.CreateConnection())
            {
                var allPositions = await connection.QueryAsync<JobPosting>(query);
                return allPositions.ToList();


            }
        }
        public async Task<IEnumerable<JobPostingDailySearchByPosition>> DailySearchByPosition(string position)
        {
            var query = $"SELECT DateTime, JobPosting.Position, JobPosting.Department, Candidate.FirstName, Candidate.LastName, Location.Building " +
                $"FROM Interview " +
                $"INNER JOIN Candidate ON Interview.CandidateId = Candidate.Id " +
                $"INNER JOIN JobPosting ON JobPosting.Id = Interview.JobId " +
                $"INNER JOIN Location ON Interview.LocationId = Location.Id " +
                $"WHERE Position = '{position}'";

            using (sqlWrapper.CreateConnection())
            {
                var candidates = await sqlWrapper.QueryAsync<JobPostingDailySearchByPosition>(query);
                return candidates.ToList();
            }
        }
    }
}