using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Job_Board.Models;

namespace Job_Board.Daos
{
    public class CandidateDao
    {
        private readonly DapperContext _context;

        public CandidateDao(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Candidate>> GetCandidates()
        {
            var query = "SELECT * FROM Candidate";
            using (var connection = _context.CreateConnection())
            {
                var candidates = await connection.QueryAsync<Candidate>(query);

                return candidates.ToList();
            }
        }
    }
}
