using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Wrappers
{
    public class SqlWrapper : ISqlWrapper
    {
        public static string ConnectionString;

        public SqlWrapper()
        {
            ConnectionString = "JobBoardConnectionString";
        }

        Task<List<T>> ISqlWrapper.Query<T>(string sql)
        {
            throw new System.NotImplementedException();
        }
    }
}
