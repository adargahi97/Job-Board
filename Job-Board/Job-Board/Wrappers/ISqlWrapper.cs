﻿using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Job_Board.Wrappers
{
    public interface ISqlWrapper
    {
        IDbConnection CreateConnection();
        Task<List<T>> QueryAsync<T>(string sql);
        Task ExecuteAsync(string sql);
        Task ExecuteAsync(string sql, DynamicParameters parameters);
        Task<T> QueryFirstOrDefaultAsync<T>(string sql);
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, DynamicParameters parameters);

    }
}