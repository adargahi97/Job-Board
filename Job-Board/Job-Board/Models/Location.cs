using Microsoft.CodeAnalysis;
using System;

namespace Job_Board.Models
{
    public class Location
    {
        public Guid Id { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string Building { get; set; }

    }
    public class LocationRequest
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string Building { get; set; }
    }

    public class LocationByBuilding
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public Guid Id { get; set; }

    }

    public class LocationByState
    {
        public string Building { get; set; }
        public string City { get; set; }

    }
}
