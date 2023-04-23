using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Job_Board.Models;
using Job_Board.Wrappers;
using System;

namespace Job_Board.Daos
{
    public class LocationDao : ILocationDao
    {

        private readonly ISqlWrapper sqlWrapper;

        public LocationDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }


        //POST Request (Create)
        public async Task CreateLocation(LocationRequest location)
        {
            var query = "INSERT INTO Location (StreetAddress, City, State, Zip, Building) " +
                "VALUES (@StreetAddress, @City, @State, @Zip, @Building)";

            var parameters = new DynamicParameters();
            parameters.Add("StreetAddress", location.StreetAddress, DbType.String);
            parameters.Add("City", location.City, DbType.String);
            parameters.Add("State", location.State, DbType.String);
            parameters.Add("Zip", location.Zip, DbType.Int32);
            parameters.Add("Building", location.Building, DbType.String);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);

            }
        }

        //GET Request
        public async Task<LocationRequest> GetLocationById(Guid id)
        {
            var query = $"SELECT * FROM Location WHERE Id = '{id}'";

            using (sqlWrapper.CreateConnection())
            {
                var location = await sqlWrapper.QueryFirstOrDefaultAsync<LocationRequest>(query);
                return location;
            }
        }

        //DELETE Request
        public async Task DeleteLocationById(Guid id)
        {
            var query = $"DELETE FROM Location WHERE Id = '{id}'";

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query);

            }
        }

        //PATCH Request (Update)
        public async Task<Location> UpdateLocationById(Location location)
        {
            var query = $"UPDATE Location SET StreetAddress = ISNULL(@StreetAddress, StreetAddress), City = ISNULL(@City, City), " +
                $"State = ISNULL(@State, State), Zip = ISNULL(@Zip, Zip), Building = ISNULL(@Building, Building) " +
                $"WHERE Id = '{location.Id}'";

            var parameters = new DynamicParameters();
            parameters.Add("StreetAddress", location.StreetAddress, DbType.String);
            parameters.Add("City", location.City, DbType.String);
            parameters.Add("State", location.State, DbType.String);
            parameters.Add("Zip", location.Zip, DbType.Int32);
            parameters.Add("Building", location.Building, DbType.String);

            using (var connection = sqlWrapper.CreateConnection())
            {
                var locationToUpdate = await connection.QueryFirstOrDefaultAsync<Location>(query, parameters);


                return locationToUpdate;
            }

        }
        //GET Request (Get Location info based on Building)
        public async Task<IEnumerable<Location>> GetAddressByBuilding(string building)
        {
            var query = $"SELECT * FROM Location WHERE Building = '{building}'";

            using (sqlWrapper.CreateConnection())
            {
                var candidates = await sqlWrapper.QueryAsync<Location>(query);
                return candidates.ToList();
            }

        }
    }
}