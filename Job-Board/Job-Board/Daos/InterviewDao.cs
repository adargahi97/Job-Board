using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Job_Board.Models;
using Job_Board.Wrappers;

namespace Job_Board.Daos
{
    public class InterviewDao : IInterviewDao
    {
        private readonly DapperContext _context;
        private readonly ISqlWrapper sqlWrapper;

        public InterviewDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }
        public InterviewDao()
        {
        }

        public InterviewDao(DapperContext context)
        {
            _context = context;
        }

        public void GetInterview()
        {
            sqlWrapper.Query<Candidate>("SELECT * FROM [DBO].[JOBBOARD]");

        }

        public async Task<IEnumerable<Interview>> GetInterviews()
        {
            var query = "SELECT * FROM Interview";
            using (var connection = _context.CreateConnection())
            {
                var interviews = await connection.QueryAsync<Interview>(query);

                return interviews.ToList();
            }
        }
    }
}
