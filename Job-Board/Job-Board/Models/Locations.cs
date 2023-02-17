using Microsoft.CodeAnalysis;

namespace Job_Board.Models
{
    public class Locations
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string Building { get; set; }
        public Locations NewLocations { get; private set; }

        public void AddLocations(Locations expectedLocations)
        {
            NewLocations = expectedLocations;
        }
    }
    public class LocationsRequest
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string Building { get; set; }
    }
}
