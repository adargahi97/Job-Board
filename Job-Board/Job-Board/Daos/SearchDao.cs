using Dapper;
using Job_Board.Models;
using Job_Board.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Job_Board.Daos
{
    public class SearchDao : ISearchDao
    {

        private readonly ISqlWrapper sqlWrapper;

        public SearchDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        public async Task<IEnumerable<LocationByState>> GetLocationByState(string state)
        {
            var query = $"SELECT * FROM Location WHERE State = '{state}'";

            using (sqlWrapper.CreateConnection())
            {
                var location = await sqlWrapper.QueryAsync<LocationByState>(query);
                return location.ToList();
            }
        }

        public async Task<IEnumerable<CandidateByLastName>> GetCandidateByLastName(string lastName)
        {
            var query = $"SELECT FirstName, LastName, PhoneNumber, JobPosting.Position, JobPosting.Department " +
                $"FROM Candidate INNER JOIN JobPosting ON Candidate.JobId = JobPosting.Id " +
                $"WHERE LastName = '{lastName}%'";

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
        public async Task<InterviewRequest> GetInterviewByCandidateId(Guid candidateId)
        {
            var query = $"SELECT Id, CONVERT(VARCHAR(20),DateTime,0) AS DateTime, JobId, LocationId, CandidateId WHERE CandidateId = '{candidateId}'";

            using (sqlWrapper.CreateConnection())
            {
                var interview = await sqlWrapper.QueryFirstOrDefaultAsync<InterviewRequest>(query);
                return interview;
            }
        }

        public async Task<IEnumerable<Interview>> GetInterviewByJobId(Guid jobId)
        {
            var query = $"SELECT Id, CONVERT(VARCHAR(20),DateTime,0) AS DateTime, JobId, LocationId, CandidateId WHERE JobId = '{jobId}'";

            using (var connection = sqlWrapper.CreateConnection())
            {
                var interviews = await connection.QueryAsync<Interview>(query);

                return interviews.ToList();
            }
        }

        public async Task<IEnumerable<InterviewJoinCandidate>> GetInterviewByLastName(string lastName)
        {

            var query = $"SELECT Candidate.FirstName, Candidate.LastName, CONVERT(VARCHAR(20),DateTime,0) AS DateTime, Interview.JobId, Interview.LocationId" +
                        $" FROM Interview " +
                        $"INNER JOIN Candidate ON Interview.CandidateId = Candidate.Id " +
                        $"WHERE LastName LIKE '{lastName}%'";

            using (var connection = sqlWrapper.CreateConnection())
            {
                var interviews = await connection.QueryAsync<InterviewJoinCandidate>(query);

                return interviews.ToList();
            }
        }
    }
}
