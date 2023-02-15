using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Wrappers
{
    public interface ISqlWrapper
    {
        Task<List<T>> Query<T>(string sql);
    }
}
