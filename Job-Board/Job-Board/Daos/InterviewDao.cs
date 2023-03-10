using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Job_Board.Models;
using Job_Board.Wrappers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Job_Board.Daos
{
    public class InterviewDao : IInterviewDao
    {

        private readonly ISqlWrapper sqlWrapper;

        public InterviewDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }


        //POST Request (Create)
        public async Task CreateInterview(InterviewRequest interview)
        {
            //SQL Query w/ dynamic params to be passed in
            var query = "INSERT INTO Interview (Date, Time, LocationsId, CandidateId) " +
                "VALUES (@Date, @Time, @LocationsId, @CandidateId)";

            var parameters = new DynamicParameters();
            parameters.Add("Date", interview.Date, DbType.String);
            parameters.Add("Time", interview.Time, DbType.String);
            parameters.Add("LocationsId", interview.LocationsId, DbType.Int32);
            parameters.Add("CandidateId", interview.CandidateId, DbType.Int32);

            //Connecting to DB
            using (var connection = sqlWrapper.CreateConnection())
            {
                //executing query
                await connection.ExecuteAsync(query, parameters);
            }
        }

        //GET Request (Read)
        public async Task<IEnumerable<Interview>> GetInterviews()
        {
            var query = "SELECT * FROM Interview";
            using (var connection = sqlWrapper.CreateConnection())
            {
                var interviews = await connection.QueryAsync<Interview>(query);

                return interviews.ToList();
            }
        }
        //GET Request (Read)
        public async Task<InterviewRequest> GetInterviewByID(int id)
        {
            //SQL query with passed in integer 
            var query = $"SELECT * FROM Interview WHERE Id = {id}";

            //Connect to DB
            using (var connection = sqlWrapper.CreateConnection())
            {
                //Run query, set to variable candidate
                var candidate = await connection.QueryFirstOrDefaultAsync<InterviewRequest>(query);

                //Return variable 
                return candidate;
            }
        }

        //DELETE Request
        public async Task DeleteInterviewById(int id)
        {
            //SQL Query to delete off of passed in integer
            var query = $"DELETE FROM Interview WHERE Id = {id}";

            //Connect to DB
             using (sqlWrapper.CreateConnection())
            {
                //Execute query
                await sqlWrapper.ExecuteAsync(query);
            }
        }

        //PATCH Request (Update)
        public async Task<Interview> UpdateInterviewById(Interview interview)
        {
            //SQL Query, injection with dynamic params & passed in candidate object to access id
            var query = $"UPDATE Interview SET Date = @Date, Time = @Time, " +
                $"LocationsId = @LocationsId, CandidateId = @CandidateId " +
                $"WHERE Id = {interview.Id}";

            var parameters = new DynamicParameters();
            parameters.Add("Date", interview.Date, DbType.String);
            parameters.Add("Time", interview.Time, DbType.String);
            parameters.Add("LocationsId", interview.LocationsId, DbType.Int32);
            parameters.Add("CandidateId", interview.CandidateId, DbType.Int32);

            //Connect to DB
            using (var connection = sqlWrapper.CreateConnection())
            {
                //set updated candidate to query result
                var updatedInterview = await connection.QueryFirstOrDefaultAsync<Interview>(query, parameters);

                return updatedInterview;
            }

        }


        public async Task<InterviewRequest> GetInterviewByCandidateId(int candidateId)
        {
            var query = $"SELECT * FROM Interview WHERE CandidateId = {candidateId}";

            using (sqlWrapper.CreateConnection())
            {
                var interview = await sqlWrapper.QueryFirstOrDefaultAsync<InterviewRequest>(query);
                return interview;
            }
        }

        public async Task<IEnumerable<Interview>> GetInterviewByJob_Id(int job_Id)
        {
            var query = $"SELECT * FROM Interview WHERE Job_Id = {job_Id}";

            using (var connection = sqlWrapper.CreateConnection())
            {
                var interviews = await connection.QueryAsync<Interview>(query);

                return interviews.ToList();
            }
        }
    }
}
