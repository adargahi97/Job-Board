using Job_Board.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface ILocationDao
    {
        Task<IEnumerable<Location>> GetLocation();
        Task CreateLocation(LocationRequest location);
        Task<LocationRequest> GetLocationByID(Guid id);
        Task DeleteLocationById(Guid id);
        Task<Location> UpdateLocationById(Location location);

        Task<LocationByBuilding> GetLocationByBuilding(string building);

        Task<LocationByState> GetLocationByState(string state);


    }
}
