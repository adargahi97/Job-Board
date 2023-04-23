using Job_Board.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface ILocationDao
    {
        Task CreateLocation(LocationRequest location);
        Task<LocationRequest> GetLocationById(Guid id);
        Task DeleteLocationById(Guid id);
        Task<Location> UpdateLocationById(Location location);
        Task<IEnumerable<Location>> GetAddressByBuilding(string building);

    }
}
