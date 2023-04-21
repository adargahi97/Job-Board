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

        //POST Request (Create)
        public async Task CreateCandidate(CandidateRequest candidate)
        {
            var query = "INSERT INTO Candidate (FirstName, LastName, PhoneNumber, JobId, InterviewId)" +
                "VALUES (@FirstName, @LastName, @PhoneNumber, @JobId, @InterviewId)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", candidate.FirstName, DbType.String);
            parameters.Add("LastName", candidate.LastName, DbType.String);
            parameters.Add("PhoneNumber", candidate.PhoneNumber, DbType.String);
            parameters.Add("JobId", candidate.JobId, DbType.Guid);
            parameters.Add("InterviewId", candidate.InterviewId, DbType.Guid);

            using (var connection = sqlWrapper.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        //GET Request (Get All Candidates)
        public async Task<IEnumerable<Candidate>> GetCandidates()
        {
            var query = "SELECT * FROM Candidate";

            using (var connection = sqlWrapper.CreateConnection())
            {
                var candidates = await connection.QueryAsync<Candidate>(query);
                return candidates.ToList();
            }
        }

        //GET Request (Return single candidate by Id)
        public async Task<Candidate> GetCandidateByID(Guid id)
        {
            var query = $"SELECT * FROM Candidate WHERE Id = '{id}'";
            using (sqlWrapper.CreateConnection())
            {
                var candidate = await sqlWrapper.QueryFirstOrDefaultAsync<Candidate>(query);
                return candidate;
            }
        }

        //DELETE Request (Delete on Id)
        public async Task DeleteCandidateById(Guid id)
        {
            var query = $"DELETE FROM Candidate WHERE Id = '{id}'";

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query);
            }
        }

        //PATCH Request (Update on Id)
        public async Task<Candidate> UpdateCandidateById(Candidate candidate)
        {
            var query = $"UPDATE Candidate SET FirstName = ISNULL(@FirstName, FirstName), LastName = ISNULL(@LastName, LastName), " +
                $"PhoneNumber = ISNULL(@PhoneNumber, PhoneNumber), JobId = ISNULL(@JobId, JobId)" +
                $"WHERE Id = '{candidate.Id}'";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", candidate.FirstName, DbType.String);
            parameters.Add("LastName", candidate.LastName, DbType.String);
            parameters.Add("PhoneNumber", candidate.PhoneNumber, DbType.String);

            if (candidate.JobId == Guid.Empty)
            {
                parameters.Add("JobId", DBNull.Value, DbType.Guid);
            }
            else
            {
                parameters.Add("JobId", candidate.JobId, DbType.Guid);
            }

            using (var connection = sqlWrapper.CreateConnection())
            {
                var updatedCandidate = await connection.QueryFirstOrDefaultAsync<Candidate>(query, parameters);
                return updatedCandidate;
            }

        }
    }
}