using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Wrappers
{
    public interface ISqlWrapper
    {
        //void ExecuteAsync(string query);
        Task<List<T>> Query<T>(string sql);
        //void QueryFirstOrDefaultAsync<T>(string v, object value);
    }
}
