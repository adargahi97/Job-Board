using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Job_Board.Models;
using Job_Board.Wrappers;

namespace Job_Board.Daos
{
    public class JobPostingDao : IJobPostingDao
    {
        private readonly DapperContext _context;
        private readonly ISqlWrapper sqlWrapper;

        public JobPostingDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }
        public JobPostingDao(DapperContext context)
        {
            _context = context;
        }

        public JobPostingDao()
        {

        }

        public void GetJobPosting()
        {
            sqlWrapper.Query<Candidate>("SELECT * FROM [DBO].[JOBBOARD]");

        }

        public async Task<IEnumerable<JobPosting>> GetJobPostings()
        {
            var query = "SELECT * FROM Job_Posting";
            using (var connection = _context.CreateConnection())
            {
                var jobPostings = await connection.QueryAsync<JobPosting>(query);

                return jobPostings.ToList();
            }
        }
    }
}
