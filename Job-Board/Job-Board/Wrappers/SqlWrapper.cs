using System.Collections.Generic;

namespace Job_Board.Wrappers
{
    public class SqlWrapper : ISqlWrapper
    {
        public static string ConnectionString;

        public SqlWrapper()
        {
            ConnectionString = "JobBoardConnectionString";
        }

        public List<T> Query<T>(string sql)
        {
            throw new System.NotImplementedException();
        }
    }
}
