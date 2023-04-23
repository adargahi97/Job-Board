using Dapper;
using Job_Board.Models;
using Job_Board.Wrappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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
            var query = "INSERT INTO Candidate (FirstName, LastName, PhoneNumber, JobId)" +
                "VALUES (@FirstName, @LastName, @PhoneNumber, @JobId)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", candidate.FirstName, DbType.String);
            parameters.Add("LastName", candidate.LastName, DbType.String);
            parameters.Add("PhoneNumber", candidate.PhoneNumber, DbType.String);
            parameters.Add("JobId", candidate.JobId, DbType.Guid);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }

        //GET Request (Return single candidate by Id)
        public async Task<Candidate> GetCandidateById(Guid id)
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
        //GET Request (Return candidate(s) by Last Name)
        public async Task<IEnumerable<CandidateJoin>> GetCandidateByLastName(string lastName)
        {
            var query = $"SELECT Candidate.Id, FirstName, LastName, PhoneNumber, JobPosting.Position, JobPosting.Department " +
                $"FROM Candidate INNER JOIN JobPosting ON Candidate.JobId = JobPosting.Id " +
                $"WHERE LastName LIKE '{lastName}%'";
            using (sqlWrapper.CreateConnection())
            {
                var candidates = await sqlWrapper.QueryAsync<CandidateJoin>(query);
                return candidates.ToList();
            }

        }
        //GET Request (Return candidate(s) by Phone Number)
        public async Task<IEnumerable<CandidateJoin>> GetCandidateByPhoneNumber(string phoneNumber)
        {
            var query = $"SELECT Candidate.Id, FirstName, LastName, PhoneNumber, JobPosting.Position, JobPosting.Department " +
                $"FROM Candidate INNER JOIN JobPosting ON Candidate.JobId = JobPosting.Id " +
                $"WHERE PhoneNumber LIKE '%{phoneNumber}%'";
            using (sqlWrapper.CreateConnection())
            {
                var candidates = await sqlWrapper.QueryAsync<CandidateJoin>(query);
                return candidates.ToList();
            }

        }
    }
}