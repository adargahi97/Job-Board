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

        //POST Request (Create a new Job Posting)
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
        //GET Request (Get a specific Job Posting by Id)
        public async Task<JobPostingRequest> GetJobPostingById(Guid id)
        {
            var query = $"SELECT * FROM JobPosting WHERE Id = '{id}'";

            using (sqlWrapper.CreateConnection())
            {
                var jobPosting = await sqlWrapper.QueryFirstOrDefaultAsync<JobPostingRequest>(query);

                return jobPosting;
            }
        }
        //DELETE Request (Delete an entry based off of Job Id)
        public async Task DeleteJobPostingById(Guid id)
        {
            var query = $"DELETE FROM JobPosting WHERE Id = '{id}'";

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query);
            }
        }
        //PATCH Request (Update a Job Posting based off of Id)
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
        //GET Request (Get Job Postings by Position)
        public async Task<JobPostingJoin> GetJobPostingByPosition(string position)
        {
            var query = $"SELECT JobPosting.Id, JobPosting.Position, JobPosting.Department, JobPosting.Description, City, State, Building FROM Location " +
                $"INNER JOIN JobPosting ON Location.Id = JobPosting.LocationId " +
                $" WHERE JobPosting.Position = '{position}' " +
                $"Order By City";

            using (sqlWrapper.CreateConnection())
            {
                var jobPosting = await sqlWrapper.QueryFirstOrDefaultAsync<JobPostingJoin>(query);
                return jobPosting;
            }
        }
        //GET Request (Get Job Postings by Building)
        public async Task<IEnumerable<JobPostingJoin>> GetJobPostingByBuilding(string building)
        {
            var query = $"SELECT JobPosting.Id, JobPosting.Position, JobPosting.Department, JobPosting.Description, City, State, Building FROM Location " +
                $"INNER JOIN JobPosting ON Location.Id = JobPosting.LocationId " +
                $" WHERE Building = '{building}' " +
                $"Order By JobPosting.Position";
            using (sqlWrapper.CreateConnection())
            {
                var location = await sqlWrapper.QueryAsync<JobPostingJoin>(query);
                return location.ToList();
            }
        }
    }
}