using Microsoft.CodeAnalysis;
using System;

namespace Job_Board.Models
{
    public class Locations
    {
        public Guid Id { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string Building { get; set; }

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
