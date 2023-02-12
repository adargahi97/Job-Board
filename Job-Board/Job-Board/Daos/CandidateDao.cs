using Dapper;
using Job_Board.Models;
using Job_Board.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public class CandidateDao : ICandidateDao
    {
        private readonly DapperContext _context;
        private readonly ISqlWrapper sqlWrapper;


        public CandidateDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        public CandidateDao(DapperContext context)
        {
            _context = context;
        }

        public void GetCandidate()
        {

        }

        public async Task CreateCandidate(CandidateRequest candidate)
        {
            var query = "INSERT INTO Candidate (FirstName, LastName, PhoneNumber, Job_Id, LocationsId) " +
                "VALUES (@FirstName, @LastName, @PhoneNumber, @Job_Id, @LocationsId)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", candidate.FirstName, DbType.String);
            parameters.Add("LastName", candidate.LastName, DbType.String);
            parameters.Add("PhoneNumber", candidate.PhoneNumber, DbType.String);
            parameters.Add("Job_Id", candidate.Job_Id, DbType.Int32);
            parameters.Add("LocationsId", candidate.LocationsId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<IEnumerable<Candidate>> GetCandidates()
        {
            var query = "SELECT * FROM Candidate";
            using (var connection = _context.CreateConnection())
            {
                var candidates = await connection.QueryAsync<Candidate>(query);

                return candidates.ToList();
            }
        }

        public async Task<CandidateRequest> GetCandidateByID(int id)
        {
            var query = $"SELECT * FROM Candidate WHERE Id = {id}";

            using (var connection = _context.CreateConnection())
            {
                var candidate = await connection.QueryFirstOrDefaultAsync<CandidateRequest>(query);
                return candidate;
            }
        }

        public async Task DeleteCandidateById(int id)
        {
            var query = $"DELETE FROM Candidate WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }

        }

        public async Task<Candidate> UpdateCandidateById(Candidate candidate)
        {
            var query = $"UPDATE Candidate SET FirstName = @FirstName, LastName = @LastName, " +
                $"PhoneNumber = @PhoneNumber, Job_Id = @Job_Id, LocationsId = @LocationsId " +
                $"WHERE Id = {candidate.Id}";



            var parameters = new DynamicParameters();
            parameters.Add("FirstName", candidate.FirstName, DbType.String);
            parameters.Add("LastName", candidate.LastName, DbType.String);
            parameters.Add("PhoneNumber", candidate.PhoneNumber, DbType.String);
            parameters.Add("Job_Id", candidate.Job_Id, DbType.Int32);
            parameters.Add("LocationsId", candidate.LocationsId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var updatedCandidate = await connection.QueryFirstOrDefaultAsync<Candidate>(query, parameters);
                return updatedCandidate;
            }

        }
    }
}
