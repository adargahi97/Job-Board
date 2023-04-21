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

        //Create a new Entry
        public async Task CreateJobPosting(JobPostingRequest jobPosting)
        {
            var query = "INSERT INTO JobPosting (Position, LocationId, Department, Description) " +
                "VALUES (@Position, @LocationId, @Department, @Description)";

            var parameters = new DynamicParameters();
            parameters.Add("Position", jobPosting.Position, DbType.String);
            parameters.Add("LocationId", jobPosting.LocationId, DbType.Guid);
            parameters.Add("Department", jobPosting.Department, DbType.String);
            parameters.Add("Description", jobPosting.Description, DbType.String);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }
        public async Task<IEnumerable<JobPosting>> GetJobPostings()
        {
            var query = "SELECT * FROM JobPosting";
            using (sqlWrapper.CreateConnection())
            {
                var jobPostings = await sqlWrapper.QueryAsync<JobPosting>(query);

                return jobPostings.ToList();
            }
        }
        public async Task<JobPostingRequest> GetJobPostingByID(Guid id)
        {
            var query = $"SELECT * FROM JobPosting WHERE Id = '{id}'";

            using (sqlWrapper.CreateConnection())
            {
                var jobPosting = await sqlWrapper.QueryFirstOrDefaultAsync<JobPostingRequest>(query);

                return jobPosting;
            }
        }

        public async Task DeleteJobPostingById(Guid id)
        {
            var query = $"DELETE FROM JobPosting WHERE Id = '{id}'";

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query);
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

            using (sqlWrapper.CreateConnection())
            {
                var updatedJobPosting = await sqlWrapper.QueryFirstOrDefaultAsync<JobPosting>(query, parameters);

                return updatedJobPosting;
            }

        }
    }
}