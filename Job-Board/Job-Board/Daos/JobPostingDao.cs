using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Job_Board.Models;
using Job_Board.Wrappers;
using System;

namespace Job_Board.Daos
{
    public class JobPostingDao : IJobPostingDao
    {
        private readonly ISqlWrapper sqlWrapper;

        public JobPostingDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }


        public async Task CreateJobPosting(JobPostingRequest jobPosting)
        {
            var query = "INSERT INTO JobPosting (Position, LocationId, Department, Description) " +
                "VALUES (@Position, @LocationId, @Department, @Description)";

            var parameters = new DynamicParameters();
            parameters.Add("Position", jobPosting.Position, DbType.String);
            parameters.Add("LocationId", jobPosting.LocationId, DbType.Guid);
            parameters.Add("Department", jobPosting.Department, DbType.String);
            parameters.Add("Description", jobPosting.Description, DbType.String);

            using (var connection = sqlWrapper.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<IEnumerable<JobPosting>> GetJobPostings()
        {
            var query = "SELECT * FROM JobPosting";
            using (var connection = sqlWrapper.CreateConnection())
            {
                var jobPostings = await connection.QueryAsync<JobPosting>(query);

                return jobPostings.ToList();
            }
        }
        public async Task<JobPostingRequest> GetJobPostingByID(Guid id)
        {
            var query = $"SELECT * FROM JobPosting WHERE Id = '{id}'";

            using (var connection = sqlWrapper.CreateConnection())
            {
                var jobPosting = await connection.QueryFirstOrDefaultAsync<JobPostingRequest>(query);

                return jobPosting;
            }
        }

        public async Task DeleteJobPostingById(Guid id)
        {
            var query = $"DELETE FROM JobPosting WHERE Id = '{id}'";

            using (var connection = sqlWrapper.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task<JobPosting> UpdateJobPostingById(JobPosting jobPosting)
        {
            var query = $"UPDATE JobPosting SET Position = ISNULL(@Position, Position), LocationId = COALESCE(@LocationId, LocationId), " +
                $"Department = ISNULL(@Department, Department), Description = ISNULL(@Description, Description)" +
                $"WHERE Id = '{jobPosting.Id}'";

            var parameters = new DynamicParameters();
            parameters.Add("Position", jobPosting.Position, DbType.String);
            if (jobPosting.LocationId == Guid.Empty)
            {
                parameters.Add("LocationId", DBNull.Value, DbType.Guid);
            }
            else
            {
                parameters.Add("LocationId", jobPosting.LocationId, DbType.Guid);
            }
            parameters.Add("Department", jobPosting.Department, DbType.String);
            parameters.Add("Description", jobPosting.Description, DbType.String);

            using (var connection = sqlWrapper.CreateConnection())
            {
                var updatedJobPosting = await connection.QueryFirstOrDefaultAsync<JobPosting>(query, parameters);

                return updatedJobPosting;
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

        public async Task<JobPostingByLocationId> GetJobPostingByLocationId(Guid locationId)
        {
            var query = $"SELECT * FROM JobPosting WHERE LocationId = '{locationId}'";

            using (sqlWrapper.CreateConnection())
            {
                var jobPosting = await sqlWrapper.QueryFirstOrDefaultAsync<JobPostingByLocationId>(query);
                return jobPosting;
            }
        }






    }
}