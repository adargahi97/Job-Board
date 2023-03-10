using Job_Board.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface ILocationsDao
    {
        Task<IEnumerable<Locations>> GetLocations();
        Task CreateLocation(LocationsRequest location);
        Task<LocationsRequest> GetLocationsByID(int id);
        Task DeleteLocationsById(int id);
        Task<Locations> UpdateLocationById(Locations location);

    }
}
