using Dapper;
using Job_Board.Models;
using Job_Board.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Job_Board.Daos
{
    public class SearchDao
    {

        private readonly ISqlWrapper sqlWrapper;

        public SearchDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        public async Task<IEnumerable<JobPostingByState>> GetJobPostingByState(string state)
        {
            var query = $"SELECT JobPosting.Position, JobPosting.Department, City, Building FROM Location " +
                $"INNER JOIN JobPosting ON Location.Id = JobPosting.LocationId " +
                $" WHERE State = '{state}' " +
                $"Order By City";
            using (sqlWrapper.CreateConnection())
            {
                var location = await sqlWrapper.QueryAsync<JobPostingByState>(query);
                return location.ToList();
            }
        }

        public async Task<IEnumerable<CandidateByLastName>> GetCandidateByLastName(string lastName)
        {
            var query = $"SELECT FirstName, LastName, PhoneNumber, JobPosting.Position, JobPosting.Department " +
                $"FROM Candidate INNER JOIN JobPosting ON Candidate.JobId = JobPosting.Id " +
                $"WHERE LastName = '{lastName}'";
            using (sqlWrapper.CreateConnection())
            {
                var candidates = await sqlWrapper.QueryAsync<CandidateByLastName>(query);
                return candidates.ToList();
            }
        }

        public async Task<IEnumerable<CandidateByJobId>> GetCandidateByJobId(Guid jobId)
        {
            var query = $"SELECT FirstName, LastName, PhoneNumber FROM Candidate WHERE JobId = '{jobId}'";

            using (sqlWrapper.CreateConnection())
            {
                var candidates = await sqlWrapper.QueryAsync<CandidateByJobId>(query);
                return candidates.ToList();
            }
        }

        public async Task<IEnumerable<InterviewRequest>> GetInterviewByJobId(Guid jobId)
        {
            var query = $" SELECT CONVERT(VARCHAR(20),Interview.DateTime,0) AS DateTime, JobId, Interview.LocationId, Interview.CandidateId " +
                $"FROM JobPosting " +
                $"INNER JOIN Interview ON JobPosting.ID = Interview.JobId " +
                $"WHERE JobId = '{jobId}'";

            using (sqlWrapper.CreateConnection())
            {
                var interviews = await sqlWrapper.QueryAsync<InterviewRequest>(query);

                return interviews.ToList();
            }
        }

        public async Task<IEnumerable<InterviewJoinCandidate>> GetInterviewByLastName(string lastName)
        {

            var query = $"SELECT Candidate.FirstName, Candidate.LastName, CONVERT(VARCHAR(20),DateTime,0) AS DateTime, Interview.JobId, Interview.LocationId" +
                        $" FROM Interview " +
                        $"INNER JOIN Candidate ON Interview.CandidateId = Candidate.Id " +
                        $"WHERE LastName LIKE '{lastName}%'";

            using (sqlWrapper.CreateConnection())
            {
                var interviews = await sqlWrapper.QueryAsync<InterviewJoinCandidate>(query);

                return interviews.ToList();
            }
        }
        public async Task<JobPostingByPosition> GetJobPostingByPosition(string position)
        {
            var query = $"SELECT * FROM JobPosting WHERE Position = '{position}'";
            using (sqlWrapper.CreateConnection())
            {
                var jobPosting = await sqlWrapper.QueryFirstOrDefaultAsync<JobPostingByPosition>(query);
                return jobPosting;
            }
        }
        public async Task<IEnumerable<JobPostingByLocationId>> GetJobPostingByLocationId(Guid locationId)
        {
            var query = $"SELECT * FROM JobPosting WHERE LocationId = '{locationId}'";
            using (sqlWrapper.CreateConnection())
            {
                var jobPosting = await sqlWrapper.QueryAsync<JobPostingByLocationId>(query);
                return jobPosting.ToList();
            }
        }
    }
}
