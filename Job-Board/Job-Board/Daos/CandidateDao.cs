using Job_Board.Wrappers;
using System;

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



    }
}
