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

        //GET Request
        public async Task<IEnumerable<Location>> GetLocation()
        {
            var query = "SELECT * FROM Location";
            using (sqlWrapper.CreateConnection())
            {
                var employees = await sqlWrapper.QueryAsync<Location>(query);

                return employees.ToList();
            }
        }

        //POST Request (Create)
        public async Task CreateLocation(LocationRequest location)
        {
            //SQL Query
            var query = "INSERT INTO Location (StreetAddress, City, State, Zip, Building) " +
                "VALUES (@StreetAddress, @City, @State, @Zip, @Building)";

            var parameters = new DynamicParameters();
            parameters.Add("StreetAddress", location.StreetAddress, DbType.String);
            parameters.Add("City", location.City, DbType.String);
            parameters.Add("State", location.State, DbType.String);
            parameters.Add("Zip", location.Zip, DbType.Int32);
            parameters.Add("Building", location.Building, DbType.String);

            //Connecting to DB
            using (sqlWrapper.CreateConnection())
            {
                //executing query
                await sqlWrapper.ExecuteAsync(query, parameters);

            }
        }

        //GET Request
        public async Task<LocationRequest> GetLocationByID(Guid id)
        {
            //SQL query
            var query = $"SELECT * FROM Location WHERE Id = '{id}'";

            //Connect to DB
            using (sqlWrapper.CreateConnection())
            {
                //Run query, set to variable candidate
                var location = await sqlWrapper.QueryFirstOrDefaultAsync<LocationRequest>(query);

                //Return variable 

                return location;
            }
        }

        //DELETE Request
        public async Task DeleteLocationById(Guid id)
        {
            //SQL Query
            var query = $"DELETE FROM Location WHERE Id = '{id}'";

            //Connect to DB
            using (sqlWrapper.CreateConnection())
            {
                //Execute query
                await sqlWrapper.ExecuteAsync(query);

            }
        }

        //PATCH Request (Update)
        public async Task<Location> UpdateLocationById(Location location)
        {
            //SQL Query
            var query = $"UPDATE Location SET StreetAddress = ISNULL(@StreetAddress, StreetAddress), City = ISNULL(@City, City), " +
                $"State = ISNULL(@State, State), Zip = ISNULL(@Zip, Zip), Building = ISNULL(@Building, Building) " +
                $"WHERE Id = '{location.Id}'";

            var parameters = new DynamicParameters();
            parameters.Add("StreetAddress", location.StreetAddress, DbType.String);
            parameters.Add("City", location.City, DbType.String);
            parameters.Add("State", location.State, DbType.String);
            parameters.Add("Zip", location.Zip, DbType.Int32);
            parameters.Add("Building", location.Building, DbType.String);

            //Connect to DB
            using (var connection = sqlWrapper.CreateConnection())
            {
                //set updated candidate to query result
                var locationToUpdate = await connection.QueryFirstOrDefaultAsync<Location>(query, parameters);


                return locationToUpdate;
            }

        }
    }
}