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

            var query = $"SELECT Id, CONVERT(VARCHAR(20),DateTime,0) AS DateTime, JobId, LocationId, CandidateId FROM Interview WHERE '{dt}' = CAST(DateTime AS DATE)";

            using (sqlWrapper.CreateConnection())
            {
                var interviews = await sqlWrapper.QueryAsync<Interview>(query);

                return interviews.ToList();
            }
        }

        public async Task<IEnumerable<InterviewDailySearch>> GetTodaysInterviews()
        {
            var query = "SELECT Candidate.FirstName, Candidate.LastName, CONVERT(VARCHAR(15),CAST(DateTime AS TIME),100) AS DateTime, JobPosting.Position, Location.Building " +
                "FROM Interview " +
                "INNER JOIN Candidate ON Interview.CandidateId = Candidate.Id " +
                "INNER JOIN Location ON Interview.LocationId = Location.Id " +
                "INNER JOIN JobPosting ON Interview.JobId = JobPosting.Id " +
                "WHERE CAST(DateTime AS DATE) = CAST(GETDATE() AS DATE)";

            using (sqlWrapper.CreateConnection())
            {
                var interviews = await sqlWrapper.QueryAsync<InterviewDailySearch>(query);
                return interviews.ToList();
            }
        }

        public async Task<IEnumerable<JobPosting>> CheckJobPostingExists(string position)
        {

            var query = "SELECT Position FROM JobPosting";

            using (sqlWrapper.CreateConnection())
            {
                var allPositions = await sqlWrapper.QueryAsync<JobPosting>(query);
                return allPositions.ToList();
            }
        }
        public async Task<IEnumerable<JobPostingDailySearchByPosition>> DailySearchByPosition(string position)
        {
            var query = $"SELECT CONVERT(VARCHAR(20),DateTime,0) AS DateTime, JobPosting.Position, JobPosting.Department, Candidate.FirstName, Candidate.LastName, Location.Building " +
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