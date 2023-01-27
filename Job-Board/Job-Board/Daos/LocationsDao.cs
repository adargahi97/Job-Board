using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Job_Board.Models;

namespace Job_Board.Daos
{
    public class LocationsDao
    {
        private readonly DapperContext _context;

        public LocationsDao(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Locations>> GetLocations()
        {
            var query = "SELECT * FROM Locations";
            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Locations>(query);

                return employees.ToList();
            }
        }
    }
}
