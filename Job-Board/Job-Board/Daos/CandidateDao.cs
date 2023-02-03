using Job_Board.Wrappers;
using System;
﻿using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Job_Board.Models;

namespace Job_Board.Daos
{
    public class CandidateDao:ICandidateDao
    {
        private readonly DapperContext _context;
        private readonly ISqlWrapper sqlWrapper;


        public CandidateDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        public CandidateDao(DapperContext context)
        {
            _context = context;
        }

        public void GetCandidate()
        {
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
