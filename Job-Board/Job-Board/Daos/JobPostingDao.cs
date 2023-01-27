using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Job_Board.Models;

namespace Job_Board.Daos
{
    public class JobPostingDao
    {
        private readonly DapperContext _context;

        public JobPostingDao(DapperContext context)
        {
            _context = context;
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
