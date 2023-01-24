namespace Job_Board.Daos
{
    public class JobPostingDao
    {
        private readonly DapperContext _context;

        public JobPostingDao(DapperContext context)
        {
            _context = context;
        }
    }
}
    }
}
