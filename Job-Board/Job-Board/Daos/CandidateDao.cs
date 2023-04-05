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
    public class CandidateDao : ICandidateDao
    {
        private readonly ISqlWrapper sqlWrapper;

        public CandidateDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        public async Task CreateCandidate(CandidateRequest candidate)
        {
            var query = "INSERT INTO Candidate (FirstName, LastName, PhoneNumber, JobId)" +
                "VALUES (@FirstName, @LastName, @PhoneNumber, @JobId)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", candidate.FirstName, DbType.String);
            parameters.Add("LastName", candidate.LastName, DbType.String);
            parameters.Add("PhoneNumber", candidate.PhoneNumber, DbType.String);
            parameters.Add("JobId", candidate.JobId, DbType.Guid);

            using (var connection = sqlWrapper.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<IEnumerable<Candidate>> GetCandidates()
        {
            var query = "SELECT * FROM Candidate";

            using (var connection = sqlWrapper.CreateConnection())
            {
                var candidates = await connection.QueryAsync<Candidate>(query);
                return candidates.ToList();
            }
        }


        public async Task<Candidate> GetCandidateByID(Guid id)
        {
            var query = $"SELECT * FROM Candidate WHERE Id = '{id}'";
            using (sqlWrapper.CreateConnection())
            {
                var candidate = await sqlWrapper.QueryFirstOrDefaultAsync<Candidate>(query);
                return candidate;
            }
        }

        public async Task DeleteCandidateById(Guid id)
        {
            var query = $"DELETE FROM Candidate WHERE Id = '{id}'";

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query);
            }
        }

        public async Task<Candidate> UpdateCandidateById(Candidate candidate)
        {
            var query = $"UPDATE Candidate SET FirstName = @FirstName, LastName = @LastName, " +
                $"PhoneNumber = @PhoneNumber, JobId = @JobId" +
                $"WHERE Id = {candidate.Id}";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", candidate.FirstName, DbType.String);
            parameters.Add("LastName", candidate.LastName, DbType.String);
            parameters.Add("PhoneNumber", candidate.PhoneNumber, DbType.String);
            parameters.Add("JobId", candidate.JobId, DbType.Guid);

            using (var connection = sqlWrapper.CreateConnection())
            {
                var updatedCandidate = await connection.QueryFirstOrDefaultAsync<Candidate>(query, parameters);
                return updatedCandidate;
            }

        }

        public async Task<IEnumerable<CandidateByLastName>> GetCandidateByLastName(string lastName)
        {
            var query = $"SELECT FirstName, LastName, PhoneNumber, JobPosting.Position, JobPosting.Department " +
                $"FROM Candidate INNER JOIN JobPosting ON Candidate.JobId = JobPosting.Id " +
                $"WHERE LastName = '{lastName}'";

            using (var connection = sqlWrapper.CreateConnection())
            {
                var candidates = await connection.QueryAsync<CandidateByLastName>(query);
                return candidates.ToList();
            }
        }

        public async Task<IEnumerable<CandidateByJobId>> GetCandidateByJobId(Guid jobId)
        {
            var query = $"SELECT FirstName, LastName, PhoneNumber FROM Candidate WHERE JobId = '{jobId}'";

            using (var connection = sqlWrapper.CreateConnection())
            {
                var candidates = await connection.QueryAsync<CandidateByJobId>(query);
                return candidates.ToList();
            }
        }
    }
}