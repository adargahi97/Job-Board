using Job_Board.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.UnitTest
{
    public class MockSqlWrapper : ISqlWrapper
    {
        public List<T> Query<T>(string sql)
        {
            return new List<T>();
        }

        Task<List<T>> ISqlWrapper.Query<T>(string sql)
        {
            throw new NotImplementedException();
        }
    }
}
