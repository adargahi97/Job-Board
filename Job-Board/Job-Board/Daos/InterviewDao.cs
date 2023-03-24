﻿using System.Collections.Generic;
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

        public async Task CreateInterview(InterviewRequest interview)
        {
            var query = "INSERT INTO Interview (Date, Time, LocationId, CandidateId) " +
                "VALUES (@Date, @Time, @LocationId, @CandidateId)";

            var parameters = new DynamicParameters();
            parameters.Add("Date", interview.Date, DbType.String);
            parameters.Add("Time", interview.Time, DbType.String);
            parameters.Add("LocationId", interview.LocationId, DbType.Guid);
            parameters.Add("CandidateId", interview.CandidateId, DbType.Guid);

            using (var connection = sqlWrapper.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<IEnumerable<Interview>> GetInterviews()
        {
            var query = "SELECT * FROM Interview";
            using (var connection = sqlWrapper.CreateConnection())
            {
                var interviews = await connection.QueryAsync<Interview>(query);

                return interviews.ToList();
            }
        }

        public async Task<InterviewRequest> GetInterviewByID(Guid id)
        {
            var query = $"SELECT * FROM Interview WHERE Id = '{id}'";

            using (var connection = sqlWrapper.CreateConnection())
            {
                var candidate = await connection.QueryFirstOrDefaultAsync<InterviewRequest>(query);
                return candidate;
            }
        }

        public async Task DeleteInterviewById(Guid id)
        {
            var query = $"DELETE FROM Interview WHERE Id = '{id}'";
            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query);
            }
        }

        public async Task<Interview> UpdateInterviewById(Interview interview)
        {
            var query = $"UPDATE Interview SET Date = @Date, Time = @Time, " +
                $"LocationId = @LocationId, CandidateId = @CandidateId " +
                $"WHERE Id = {interview.Id}";

            var parameters = new DynamicParameters();
            parameters.Add("Date", interview.Date, DbType.String);
            parameters.Add("Time", interview.Time, DbType.String);
            parameters.Add("LocationId", interview.LocationId, DbType.Guid);
            parameters.Add("CandidateId", interview.CandidateId, DbType.Guid);

            using (var connection = sqlWrapper.CreateConnection())
            {
                var updatedInterview = await connection.QueryFirstOrDefaultAsync<Interview>(query, parameters);
                return updatedInterview;
            }

        }

        public async Task<InterviewRequest> GetInterviewByCandidateId(Guid candidateId)
        {
            var query = $"SELECT * FROM Interview WHERE CandidateId = '{candidateId}'";

            using (sqlWrapper.CreateConnection())
            {
                var interview = await sqlWrapper.QueryFirstOrDefaultAsync<InterviewRequest>(query);
                return interview;
            }
        }

        public async Task<IEnumerable<Interview>> GetInterviewByJobId(Guid jobId)
        {
            var query = $"SELECT * FROM Interview WHERE JobId = '{jobId}'";

            using (var connection = sqlWrapper.CreateConnection())
            {
                var interviews = await connection.QueryAsync<Interview>(query);

                return interviews.ToList();
            }
        }


        public async Task<InterviewJoinCandidate> GetInterviewByLastName(string lastName)
        {
            //CandidateId candidateId = CandidateDao.GetCandidateIDByLastName(lastName);
            
            var query = $"SELECT Candidate.FirstName, Candidate.LastName, Interview.Date, Interview.Time, Interview.JobId, Interview.LocationId FROM Interview INNER JOIN Candidate ON Interview.CandidateId = Candidate.Id WHERE LastName = '{lastName}'";

            using (sqlWrapper.CreateConnection())
            {
                var interview = await sqlWrapper.QueryFirstOrDefaultAsync<InterviewJoinCandidate>(query);
                return interview;

            }
        }

    }
}