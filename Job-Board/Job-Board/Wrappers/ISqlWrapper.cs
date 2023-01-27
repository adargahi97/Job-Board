using System.Collections.Generic;

namespace Job_Board.Wrappers
{
    public interface ISqlWrapper
    {
        List<T> Query<T>(string sql);
    }
}
