using Dapper;
using Job_Board.Models;
using Job_Board.Wrappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Job_Board.Daos
{
    public class SearchDao : ISearchDao
    {

        private readonly ISqlWrapper sqlWrapper;

        public SearchDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        public async Task<IEnumerable<LocationByState>> GetLocationByState(string state)
        {
            var query = $"SELECT * FROM Location WHERE State = '{state}'";

            using (sqlWrapper.CreateConnection())
            {
                var location = await sqlWrapper.QueryAsync<LocationByState>(query);
                return location.ToList();
            }
        }

    }
}
