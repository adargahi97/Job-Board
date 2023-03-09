using Job_Board.Models;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface ILocationsDao
    {
        //void GetLocations();

        Task DeleteLocationsById(int id);

        Task<LocationsRequest> GetLocationsByID(int id);





        Task<Locations> UpdateLocationById(Locations location);
    }
}
