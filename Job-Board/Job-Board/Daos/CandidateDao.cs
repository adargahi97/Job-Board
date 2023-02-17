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
        //Constructor for Dapper
        public CandidateDao(DapperContext context)
        {
            _context = context;
        }

        //Test setup
        private readonly DapperContext _context;
        private readonly ISqlWrapper sqlWrapper;

        public CandidateDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        public CandidateDao()
        {
        }

        public void GetCandidate()
        {
            sqlWrapper.Query<Candidate>("SELECT * FROM [DBO].[JOBBOARD]");
        }
        
        //POST Request (Create)
        public async Task CreateCandidate(CandidateRequest candidate)
        {
            //SQL Query w/ dynamic params to be passed in
            var query = "INSERT INTO Candidate (FirstName, LastName, PhoneNumber, Job_Id, LocationsId) " +
                "VALUES (@FirstName, @LastName, @PhoneNumber, @Job_Id, @LocationsId)";

            //Parameters to be injected in the Query
            var parameters = new DynamicParameters();
            parameters.Add("FirstName", candidate.FirstName, DbType.String);
            parameters.Add("LastName", candidate.LastName, DbType.String);
            parameters.Add("PhoneNumber", candidate.PhoneNumber, DbType.String);
            parameters.Add("Job_Id", candidate.Job_Id, DbType.Int32);
            parameters.Add("LocationsId", candidate.LocationsId, DbType.Int32);

            //Connecting to DB
            using (var connection = _context.CreateConnection())
            {
                //executing query
                await connection.ExecuteAsync(query, parameters);
            }
        }

        //GET Request (Read)
        public async Task<IEnumerable<Candidate>> GetCandidates()
        {
            //SQL Query
            var query = "SELECT * FROM Candidate";

            //Connect to DB
            using (var connection = _context.CreateConnection())
            {
                //Run query, set to variable candidate
                var candidates = await connection.QueryAsync<Candidate>(query);

                //Send variable candidate to a list and return list
                return candidates.ToList();
            }
        }

        //GET Request (Read)
        public async Task<CandidateRequest> GetCandidateByID(int id)
        {
            //SQL query with passed in integer 
            var query = $"SELECT * FROM Candidate WHERE Id = {id}";

            //Connect to DB
            using (var connection = _context.CreateConnection())
            {
                //Run query, set to variable candidate
                var candidate = await connection.QueryFirstOrDefaultAsync<CandidateRequest>(query);

                //Return variable 
                return candidate;
            }
        }

        //DELETE Request
        public async Task DeleteCandidateById(int id)
        {
            //SQL Query to delete off of passed in integer
            var query = $"DELETE FROM Candidate WHERE Id = {id}";

            //Connect to DB
            using (var connection = _context.CreateConnection())
            {
                //Execute query
                await connection.ExecuteAsync(query);
            }
        }

        //PATCH Request (Update)
        public async Task<Candidate> UpdateCandidateById(Candidate candidate)
        {
            //SQL Query, injection with dynamic params & passed in candidate object to access id
            var query = $"UPDATE Candidate SET FirstName = @FirstName, LastName = @LastName, " +
                $"PhoneNumber = @PhoneNumber, Job_Id = @Job_Id, LocationsId = @LocationsId " +
                $"WHERE Id = {candidate.Id}";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", candidate.FirstName, DbType.String);
            parameters.Add("LastName", candidate.LastName, DbType.String);
            parameters.Add("PhoneNumber", candidate.PhoneNumber, DbType.String);
            parameters.Add("Job_Id", candidate.Job_Id, DbType.Int32);
            parameters.Add("LocationsId", candidate.LocationsId, DbType.Int32);

            //Connect to DB
            using (var connection = _context.CreateConnection())
            {
                //set updated candidate to query result
                var updatedCandidate = await connection.QueryFirstOrDefaultAsync<Candidate>(query, parameters);

                return updatedCandidate;
            }

        }

        public async Task<CandidateRequest> GetCandidateByFirstName(string firstName)
        {
            var query = $"SELECT * FROM Candidate WHERE Id = {firstName}";

            using (var connection = _context.CreateConnection())
            {
                var candidate = await connection.QueryFirstOrDefaultAsync<CandidateRequest>(query);
                return candidate;
            }
        }
    }
}
