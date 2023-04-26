using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Job_Board.Models;
using Job_Board.Wrappers;
using System;
using Dapper;

namespace Job_Board.Daos
{
    public class InterviewDao : IInterviewDao
    {

        private readonly ISqlWrapper sqlWrapper;

        public InterviewDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        //POST Request (Create a new Interview)
        public async Task CreateInterview(InterviewRequest interview)
        {
            var query = "INSERT INTO Interview (DateTime, LocationId, CandidateId, JobId) " +
                "VALUES (@DateTime, @LocationId, @CandidateId, @JobId)";

            var parameters = new DynamicParameters();
            parameters.Add("DateTime", interview.DateTime, DbType.DateTime);
            parameters.Add("LocationId", interview.LocationId, DbType.Guid);
            parameters.Add("CandidateId", interview.CandidateId, DbType.Guid);
            parameters.Add("JobId", interview.JobId, DbType.Guid);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }

        //GET Request (Returns a single interview on Interview Id)
        public async Task<InterviewRequest> GetInterviewById(Guid id)
        {
            var query = $"SELECT Id, CONVERT(VARCHAR(20),DateTime,0) AS DateTime, JobId, LocationId, CandidateId FROM Interview WHERE Id = '{id}'";

            using (sqlWrapper.CreateConnection())
            {
                var candidate = await sqlWrapper.QueryFirstOrDefaultAsync<InterviewRequest>(query);
                return candidate;
            }
        }
        //DEL Request (Deleted on Interview Id)
        public async Task DeleteInterviewById(Guid id)
        {
            var query = $"DELETE FROM Interview WHERE Id = '{id}'";

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query);
            }
        }
        //PATCH Request (Updates Interview on Id)
        public async Task<Interview> UpdateInterviewById(Interview interview)
        {
            var query = $"UPDATE Interview SET DateTime = ISNULL(@DateTime, DateTime), " +
                $"LocationId = ISNULL(@LocationId, LocationId), CandidateId = ISNULL(@CandidateId, CandidateId), JobId = ISNULL(@JobId, JobId) " +
                $"WHERE Id = '{interview.Id}'";

            var parameters = new DynamicParameters();

            parameters.Add("DateTime", interview.DateTime, DbType.DateTime);

            if (interview.LocationId == Guid.Empty)
            {
                parameters.Add("LocationId", DBNull.Value, DbType.Guid);
            }
            else
            {
                parameters.Add("LocationId", interview.LocationId, DbType.Guid);
            }
            if (interview.CandidateId == Guid.Empty)
            {
                parameters.Add("CandidateId", DBNull.Value, DbType.Guid);
            }
            else
            {
                parameters.Add("CandidateId", interview.CandidateId, DbType.Guid);
            }
            if (interview.JobId == Guid.Empty)
            {
                parameters.Add("JobId", DBNull.Value, DbType.Guid);
            }
            else
            {
                parameters.Add("JobId", interview.JobId, DbType.Guid);
            }

            using (var connection = sqlWrapper.CreateConnection())
            {
                var updatedInterview = await connection.QueryFirstOrDefaultAsync<Interview>(query, parameters);
                return updatedInterview;
            }
        }

        //GET Request (Return Interview info based on Date)
        public async Task<IEnumerable<InterviewJoin>> GetInterviewsByDate(DateTime dt)
        {
            var query = $"SELECT Candidate.FirstName, Candidate.LastName, JobPosting.Department, CONVERT(VARCHAR(15),CAST(DateTime AS TIME),100) AS DateTime, JobPosting.Position, Location.Building " +
                $"FROM Interview " +
                $"INNER JOIN Candidate ON Interview.CandidateId = Candidate.Id " +
                $"INNER JOIN Location ON Interview.LocationId = Location.Id " +
                $"INNER JOIN JobPosting ON Interview.JobId = JobPosting.Id " +
                $"WHERE '{dt}' = CAST(DateTime AS DATE)";

            using (sqlWrapper.CreateConnection())
            {
                var interviews = await sqlWrapper.QueryAsync<InterviewJoin>(query);

                return interviews.ToList();
            }
        }
        //GET Request (Return Interview info based on Date)
        public async Task<IEnumerable<InterviewJoin>> GetInterviewsByPosition(string position)
        {
            var query = $"SELECT CONVERT(VARCHAR(20),DateTime,0) AS DateTime, JobPosting.Position, JobPosting.Department, Candidate.FirstName, Candidate.LastName, Location.Building " +
                $"FROM Interview " +
                $"INNER JOIN Candidate ON Interview.CandidateId = Candidate.Id " +
                $"INNER JOIN JobPosting ON JobPosting.Id = Interview.JobId " +
                $"INNER JOIN Location ON Interview.LocationId = Location.Id " +
                $"WHERE Position = '{position}'";

            using (sqlWrapper.CreateConnection())
            {
                var candidates = await sqlWrapper.QueryAsync<InterviewJoin>(query);
                return candidates.ToList();
            }
        }
        //internal method used for error handling
        public async Task<IEnumerable<JobPosting>> CheckJobPostingExists(string position)
        {
            var query = "SELECT Position FROM JobPosting";

            using (sqlWrapper.CreateConnection())
            {
                var allPositions = await sqlWrapper.QueryAsync<JobPosting>(query);
                return allPositions.ToList();
            }
        }
        public async Task<IEnumerable<JobPosting>> CheckJobIDExists(Guid id)
        {
            var query = "SELECT Id FROM JobPosting";

            using (sqlWrapper.CreateConnection())
            {
                var allPositions = await sqlWrapper.QueryAsync<JobPosting>(query);
                return allPositions.ToList();
            }
        }
        //GET Request (Return Interview info based on Job Id)
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
        //GET Request (Return Interview info based on Last Name)
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
    }
}